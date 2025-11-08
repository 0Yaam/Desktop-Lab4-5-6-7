using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataAccess.OL;

namespace DataAccess
{
    public class BanDA
    {
        public List<Ban> GetAll()
        {
            var list = new List<Ban>();
            using (var conn = new SqlConnection(Ultilities.ConnectionString))
            using (var cmd = new SqlCommand("SELECT ID, Name, Status, Capacity FROM Ban", conn))
            {
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        var item = new Ban();
                        item.ID = Convert.ToInt32(rd["ID"]);
                        item.Name = rd["Name"] == DBNull.Value ? "" : rd["Name"].ToString();
                        item.Status = Convert.ToInt32(rd["Status"]);
                        item.Capacity = rd["Capacity"] == DBNull.Value ? (int?)null : Convert.ToInt32(rd["Capacity"]);
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        // action: 0=Insert, 1=Update, 2=Delete
        public int Insert_Update_Delete(Ban b, int action)
        {
            using (var conn = new SqlConnection(Ultilities.ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = Ultilities.Ban_InsertUpdateDelete;

                var pId = new SqlParameter("@ID", SqlDbType.Int);
                pId.Direction = ParameterDirection.InputOutput;
                pId.Value = b.ID;
                cmd.Parameters.Add(pId);

                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 1000).Value =
                    (object)b.Name ?? DBNull.Value;
                cmd.Parameters.Add("@Status", SqlDbType.Int).Value = b.Status;
                cmd.Parameters.Add("@Capacity", SqlDbType.Int).Value =
                    b.Capacity.HasValue ? (object)b.Capacity.Value : DBNull.Value;
                cmd.Parameters.Add("@Action", SqlDbType.Int).Value = action;

                conn.Open();
                cmd.ExecuteNonQuery();

                if (action == 0) // insert
                    return cmd.Parameters["@ID"].Value == DBNull.Value ? 0 : Convert.ToInt32(cmd.Parameters["@ID"].Value);

                return b.ID != 0 ? b.ID : 1;
            }
        }
    }
}
