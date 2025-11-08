using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataAccess.OL;

namespace DataAccess
{
    public class ReportingDA
    {
        private readonly string _connStr = Ultilities.ConnectionString;

        public List<OrderLineRaw> GetOrderLines(int billId)
        {
            var list = new List<OrderLineRaw>();
            using (var conn = new SqlConnection(_connStr))
            using (var cmd = new SqlCommand(@"
                SELECT 
                    f.Name      AS FoodName,
                    f.Unit      AS Unit,
                    f.Price     AS UnitPrice,
                    d.Quantity  AS Quantity,
                    (f.Price * d.Quantity) AS LineTotal
                FROM BillDetails d
                INNER JOIN Food f ON f.ID = d.FoodID
                WHERE d.InvoiceID = @id
                ORDER BY d.ID ASC;", conn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = billId;
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new OrderLineRaw
                        {
                            FoodName = rd["FoodName"]?.ToString() ?? "",
                            Unit = rd["Unit"]?.ToString() ?? "",
                            UnitPrice = SafeInt(rd["UnitPrice"]),
                            Quantity = SafeInt(rd["Quantity"]),
                            LineTotal = SafeInt(rd["LineTotal"]),
                        });
                    }
                }
            }
            return list;
        }

        private static int SafeInt(object v)
        {
            if (v == null || v == System.DBNull.Value) return 0;
            try { return System.Convert.ToInt32(v); } catch { return 0; }
        }
    }

    public class OrderLineRaw
    {
        public string FoodName { get; set; }
        public string Unit { get; set; }
        public int UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int LineTotal { get; set; }
    }
}
