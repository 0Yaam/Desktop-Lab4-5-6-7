using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public static class DbHelper
    {
        public static SqlCommand CreateSp(this SqlConnection conn, string sp)
        {
            var cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = sp;
            return cmd;
        }

        // ExecuteNonQuery an toàn với NOCOUNT ON
        public static void SafeExecute(this SqlCommand cmd) => cmd.ExecuteNonQuery();
    }
}
