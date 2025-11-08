using DataAccess;
using DataAccess.OL;
using System.Collections.Generic;

namespace BusinessLogic
{
    public class FoodBL
    {
        // Đối tượng DataAccess
        FoodDA foodDA = new FoodDA();

        // Lấy tất cả
        public List<Food> GetAll()
        {
            return foodDA.GetAll();
        }

        // Tìm món theo ID
        public Food GetByID(int id)
        {
            List<Food> list = GetAll();
            foreach (var item in list)
            {
                if (item.ID == id)
                    return item;
            }
            return null;
        }

        // Tìm kiếm theo từ khóa
        public List<Food> Find(string key)
        {
            List<Food> list = GetAll();
            List<Food> result = new List<Food>();

            foreach (var item in list)
            {
                if (item.ID.ToString().Contains(key)
                    || item.Name.Contains(key)
                    || item.Unit.Contains(key)
                    || item.Price.ToString().Contains(key)
                    || item.Notes.Contains(key))
                {
                    result.Add(item);
                }
            }

            return result;
        }

        // Thêm
        public int Insert(Food food)
        {
            return foodDA.Insert_Update_Delete(food, 0);
        }

        // Sửa
        public int Update(Food food)
        {
            return foodDA.Insert_Update_Delete(food, 1);
        }

        // Xóa
        public int Delete(Food food)
        {
            return foodDA.Insert_Update_Delete(food, 2);
        }
    }
}
