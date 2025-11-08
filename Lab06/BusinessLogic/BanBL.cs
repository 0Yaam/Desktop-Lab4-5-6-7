using System.Collections.Generic;
using DataAccess;
using DataAccess.OL;

namespace BusinessLogic
{
    public class BanBL
    {
        private readonly BanDA _da = new BanDA();

        public List<Ban> GetAll()
        {
            return _da.GetAll();
        }

        public int Insert(Ban b)
        {
            return _da.Insert_Update_Delete(b, 0);
        }

        public int Update(Ban b)
        {
            return _da.Insert_Update_Delete(b, 1);
        }

        public int Delete(Ban b)
        {
            return _da.Insert_Update_Delete(b, 2);
        }
    }
}
