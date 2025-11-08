using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataAccess.OL;

namespace DataAccess
{
    public class RoleAccountDA
    {
        public List<RoleAccount> GetAll()
        {
            var list = new List<RoleAccount>();
            using (var conn = new SqlConnection(Ultilities.ConnectionString))
            using (var cmd = new SqlCommand("SELECT RoleID, AccountName, Actived, Notes FROM RoleAccount", conn))
            {
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        var item = new RoleAccount();
                        item.RoleID = Convert.ToInt32(rd["RoleID"]);
                        item.AccountName = rd["AccountName"].ToString();
                        item.Actived = Convert.ToBoolean(rd["Actived"]);
                        item.Notes = rd["Notes"] == DBNull.Value ? "" : rd["Notes"].ToString();
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        // action: 0=Insert, 1=Update, 2=Delete (khoá kép RoleID + AccountName)
        public int Insert_Update_Delete(RoleAccount ra, int action)
        {
            using (var conn = new SqlConnection(Ultilities.ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = Ultilities.RoleAccount_InsertUpdateDelete;

                cmd.Parameters.Add("@RoleID", SqlDbType.Int).Value = ra.RoleID;
                cmd.Parameters.Add("@AccountName", SqlDbType.NVarChar, 100).Value =
                    (object)ra.AccountName ?? DBNull.Value;
                cmd.Parameters.Add("@Actived", SqlDbType.Bit).Value = ra.Actived;
                cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 3000).Value =
                    (object)ra.Notes ?? DBNull.Value;
                cmd.Parameters.Add("@Action", SqlDbType.Int).Value = action;

                conn.Open();
                cmd.ExecuteNonQuery();

                return 1; // không exception => OK
            }
        }
    }
}
