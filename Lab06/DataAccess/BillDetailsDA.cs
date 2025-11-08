using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataAccess.OL;

namespace DataAccess
{
    public class BillDetailsDA
    {
        public List<BillDetails> GetAll()
        {
            var list = new List<BillDetails>();
            using (var conn = new SqlConnection(Ultilities.ConnectionString))
            using (var cmd = new SqlCommand("SELECT ID, InvoiceID, FoodID, Quantity FROM BillDetails", conn))
            {
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        var item = new BillDetails();
                        item.ID = Convert.ToInt32(rd["ID"]);
                        item.InvoiceID = Convert.ToInt32(rd["InvoiceID"]);
                        item.FoodID = Convert.ToInt32(rd["FoodID"]);
                        item.Quantity = Convert.ToInt32(rd["Quantity"]);
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        // action: 0=Insert, 1=Update, 2=Delete
        public int Insert_Update_Delete(BillDetails d, int action)
        {
            using (var conn = new SqlConnection(Ultilities.ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = Ultilities.BillDetails_InsertUpdateDelete;

                var pId = new SqlParameter("@ID", SqlDbType.Int);
                pId.Direction = ParameterDirection.InputOutput;
                pId.Value = d.ID;
                cmd.Parameters.Add(pId);

                cmd.Parameters.Add("@InvoiceID", SqlDbType.Int).Value = d.InvoiceID;
                cmd.Parameters.Add("@FoodID", SqlDbType.Int).Value = d.FoodID;
                cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = d.Quantity;
                cmd.Parameters.Add("@Action", SqlDbType.Int).Value = action;

                conn.Open();
                cmd.ExecuteNonQuery();

                if (action == 0)
                    return cmd.Parameters["@ID"].Value == DBNull.Value ? 0 : Convert.ToInt32(cmd.Parameters["@ID"].Value);

                return d.ID != 0 ? d.ID : 1;
            }
        }
    }
}
