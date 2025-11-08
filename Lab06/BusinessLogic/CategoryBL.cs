using DataAccess;
using DataAccess.OL;
using System.Collections.Generic;

namespace BusinessLogic
{
    public class CategoryBL
    {
        // Đối tượng DataAccess
        CategoryDA categoryDA = new CategoryDA();

        // Lấy tất cả
        public List<Category> GetAll()
        {
            return categoryDA.GetAll();
        }

        // Thêm
        public int Insert(Category category)
        {
            return categoryDA.Insert_Update_Delete(category, 0);
        }

        // Sửa
        public int Update(Category category)
        {
            return categoryDA.Insert_Update_Delete(category, 1);
        }

        // Xóa
        public int Delete(Category category)
        {
            return categoryDA.Insert_Update_Delete(category, 2);
        }
    }
}
