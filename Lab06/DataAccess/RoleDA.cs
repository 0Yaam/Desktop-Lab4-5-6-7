using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataAccess.OL;

namespace DataAccess
{
    public class RoleeDA
    {
        public List<Role> GetAll()
        {
            var list = new List<Role>();
            using (var conn = new SqlConnection(Ultilities.ConnectionString))
            using (var cmd = new SqlCommand("SELECT ID, RoleName, Path, Notes FROM Role", conn))
            {
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        var item = new Role();
                        item.ID = Convert.ToInt32(rd["ID"]);
                        item.RoleName = rd["RoleName"].ToString();
                        item.Path = rd["Path"] == DBNull.Value ? "" : rd["Path"].ToString();
                        item.Notes = rd["Notes"] == DBNull.Value ? "" : rd["Notes"].ToString();
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        // action: 0=Insert, 1=Update, 2=Delete
        public int Insert_Update_Delete(Role r, int action)
        {
            using (var conn = new SqlConnection(Ultilities.ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = Ultilities.Rolee_InsertUpdateDelete;

                var pId = new SqlParameter("@ID", SqlDbType.Int);
                pId.Direction = ParameterDirection.InputOutput;
                pId.Value = r.ID;
                cmd.Parameters.Add(pId);

                cmd.Parameters.Add("@RoleName", SqlDbType.NVarChar, 1000).Value = (object)r.RoleName ?? DBNull.Value;
                cmd.Parameters.Add("@Path", SqlDbType.NVarChar, 3000).Value = (object)r.Path ?? DBNull.Value;
                cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 3000).Value = (object)r.Notes ?? DBNull.Value;
                cmd.Parameters.Add("@Action", SqlDbType.Int).Value = action;

                conn.Open();
                cmd.ExecuteNonQuery();

                if (action == 0)
                    return cmd.Parameters["@ID"].Value == DBNull.Value ? 0 : Convert.ToInt32(cmd.Parameters["@ID"].Value);

                return r.ID != 0 ? r.ID : 1;
            }
        }
    }
}
