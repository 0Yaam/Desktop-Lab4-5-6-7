using DataAccess.OL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class FoodDA
    {
        public List<Food> GetAll()
        {
            var list = new List<Food>();
            using (var conn = new SqlConnection(Ultilities.ConnectionString))
            using (var cmd = new SqlCommand("SELECT ID, Name, Unit, FoodCategoryID, Price, Notes FROM Food WHERE IsDeleted = 0 ORDER BY Name", conn))
            {
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new Food
                        {
                            ID = (int)rd["ID"],
                            Name = rd["Name"].ToString(),
                            Unit = rd["Unit"].ToString(),
                            FoodCategoryID = rd["FoodCategoryID"] == DBNull.Value ? 0 : Convert.ToInt32(rd["FoodCategoryID"]),
                            Price = Convert.ToInt32(rd["Price"]),
                            Notes = rd["Notes"] as string
                        });
                    }
                }
            }
            return list;
        }
        public int SoftDelete(int foodId)
        {
            using (var conn = new SqlConnection(Ultilities.ConnectionString))
            using (var cmd = new SqlCommand("UPDATE Food SET IsDeleted = 1 WHERE ID = @id", conn))
            {
                cmd.Parameters.AddWithValue("@id", foodId);
                conn.Open();
                return cmd.ExecuteNonQuery(); // 1 nếu OK
            }
        }

        public int Insert_Update_Delete(Food food, int action)
        {
            using (var sqlConn = new SqlConnection(Ultilities.ConnectionString))
            using (var command = sqlConn.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = Ultilities.Food_InsertUpdateDelete;

                var pId = new SqlParameter("@ID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.InputOutput,
                    Value = food.ID
                };
                command.Parameters.Add(pId);

                command.Parameters.Add("@Name", SqlDbType.NVarChar, 1000).Value = (object)food.Name ?? DBNull.Value;
                command.Parameters.Add("@Unit", SqlDbType.NVarChar, 100).Value = (object)food.Unit ?? DBNull.Value;
                command.Parameters.Add("@FoodCategoryID", SqlDbType.Int).Value = food.FoodCategoryID;
                command.Parameters.Add("@Price", SqlDbType.Int).Value = food.Price;
                command.Parameters.Add("@Notes", SqlDbType.NVarChar, 3000).Value =
                    string.IsNullOrWhiteSpace(food.Notes) ? (object)DBNull.Value : food.Notes;

                command.Parameters.Add("@Action", SqlDbType.Int).Value = action;

                sqlConn.Open();
                int result = command.ExecuteNonQuery();

                if (result > 0 && command.Parameters["@ID"].Value != DBNull.Value)
                    return Convert.ToInt32(command.Parameters["@ID"].Value);

                return 0;
            }
        }
    }
}
