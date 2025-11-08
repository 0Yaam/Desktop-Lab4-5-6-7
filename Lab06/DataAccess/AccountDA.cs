using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataAccess.OL;

namespace DataAccess
{
    public class AccountDA
    {
        public List<Account> GetAll()
        {
            var list = new List<Account>();
            using (var conn = new SqlConnection(Ultilities.ConnectionString))
            using (var cmd = new SqlCommand("SELECT AccountName, Password, FullName, Email, Tell, DateCreated FROM Account", conn))
            {
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        var item = new Account();
                        item.AccountName = rd["AccountName"].ToString();
                        item.Password = rd["Password"].ToString();
                        item.FullName = rd["FullName"].ToString();
                        item.Email = rd["Email"] == DBNull.Value ? "" : rd["Email"].ToString();
                        item.Tell = rd["Tell"] == DBNull.Value ? "" : rd["Tell"].ToString();
                        item.DateCreated = rd["DateCreated"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rd["DateCreated"]);
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public int Insert_Update_Delete(Account a, int action)
        {
            using (var conn = new SqlConnection(Ultilities.ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = Ultilities.Account_InsertUpdateDelete;

                var pName = new SqlParameter("@AccountName", SqlDbType.NVarChar, 100);
                pName.Direction = ParameterDirection.InputOutput;
                pName.Value = (object)a.AccountName ?? "";
                cmd.Parameters.Add(pName);

                cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 200).Value = (object)a.Password ?? DBNull.Value;
                cmd.Parameters.Add("@FullName", SqlDbType.NVarChar, 1000).Value = (object)a.FullName ?? DBNull.Value;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 1000).Value = (object)a.Email ?? DBNull.Value;
                cmd.Parameters.Add("@Tell", SqlDbType.NVarChar, 200).Value = (object)a.Tell ?? DBNull.Value;
                cmd.Parameters.Add("@DateCreated", SqlDbType.SmallDateTime).Value =
                    a.DateCreated.HasValue ? (object)a.DateCreated.Value : DBNull.Value;

                cmd.Parameters.Add("@Action", SqlDbType.Int).Value = action;

                conn.Open();
                cmd.ExecuteNonQuery();

                return 1;  
            }
        }
    }
}
