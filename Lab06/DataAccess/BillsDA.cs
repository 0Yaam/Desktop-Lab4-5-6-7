using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataAccess.OL;

namespace DataAccess
{
    public class BillsDA
    {
        public List<Bills> GetAll()
        {
            var list = new List<Bills>();
            using (var conn = new SqlConnection(Ultilities.ConnectionString))
            using (var cmd = new SqlCommand(
                "SELECT ID, Name, TableID, Amount, Discount, Tax, Status, CheckoutDate, Account FROM Bills", conn))
            {
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        var item = new Bills();
                        item.ID = Convert.ToInt32(rd["ID"]);
                        item.Name = rd["Name"].ToString();
                        item.TableID = Convert.ToInt32(rd["TableID"]);
                        item.Amount = Convert.ToInt32(rd["Amount"]);
                        item.Discount = rd["Discount"] == DBNull.Value ? (double?)null : Convert.ToDouble(rd["Discount"]);
                        item.Tax = rd["Tax"] == DBNull.Value ? (double?)null : Convert.ToDouble(rd["Tax"]);
                        item.Status = Convert.ToBoolean(rd["Status"]);
                        item.CheckoutDate = rd["CheckoutDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rd["CheckoutDate"]);
                        item.Account = rd["Account"].ToString();
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        // dùng SP gộp trong Ultilities.Bills_InsertUpdateDelete (ví dụ: Bills_InsertUpdateDelete2)
        // action: 0=Insert, 1=Update, 2=Delete
        public int Insert_Update_Delete(Bills b, int action)
        {
            using (var conn = new SqlConnection(Ultilities.ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = Ultilities.Bills_InsertUpdateDelete;

                var pId = new SqlParameter("@ID", SqlDbType.Int);
                pId.Direction = ParameterDirection.InputOutput;
                pId.Value = b.ID;
                cmd.Parameters.Add(pId);

                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 1000).Value = (object)b.Name ?? DBNull.Value;
                cmd.Parameters.Add("@TableID", SqlDbType.Int).Value = b.TableID;
                cmd.Parameters.Add("@Amount", SqlDbType.Int).Value = b.Amount;
                cmd.Parameters.Add("@Discount", SqlDbType.Float).Value =
                    b.Discount.HasValue ? (object)b.Discount.Value : DBNull.Value;
                cmd.Parameters.Add("@Tax", SqlDbType.Float).Value =
                    b.Tax.HasValue ? (object)b.Tax.Value : DBNull.Value;
                cmd.Parameters.Add("@Status", SqlDbType.Bit).Value = b.Status;
                cmd.Parameters.Add("@CheckoutDate", SqlDbType.SmallDateTime).Value =
                    b.CheckoutDate.HasValue ? (object)b.CheckoutDate.Value : DBNull.Value;
                cmd.Parameters.Add("@Account", SqlDbType.NVarChar, 100).Value = (object)b.Account ?? DBNull.Value;

                cmd.Parameters.Add("@Action", SqlDbType.Int).Value = action;

                conn.Open();
                cmd.ExecuteNonQuery();

                if (action == 0)
                    return cmd.Parameters["@ID"].Value == DBNull.Value ? 0 : Convert.ToInt32(cmd.Parameters["@ID"].Value);

                return b.ID != 0 ? b.ID : 1;
            }
        }
    }
}
