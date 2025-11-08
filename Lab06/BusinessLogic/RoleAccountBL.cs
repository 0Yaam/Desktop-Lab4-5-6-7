using System.Collections.Generic;
using DataAccess;
using DataAccess.OL;

namespace BusinessLogic
{
    public class RoleAccountBL
    {
        private readonly RoleAccountDA _da = new RoleAccountDA();

        public List<RoleAccount> GetAll()
        {
            return _da.GetAll();
        }

        public int Insert(RoleAccount r)
        {
            return _da.Insert_Update_Delete(r, 0);
        }

        public int Update(RoleAccount r)
        {
            return _da.Insert_Update_Delete(r, 1);
        }

        public int Delete(RoleAccount r)
        {
            return _da.Insert_Update_Delete(r, 2);
        }
    }
}
