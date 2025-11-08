using DataAccess;
using DataAccess.OL;
using System.Collections.Generic;
using System.Data;

namespace BusinessLogic
{
    public class RoleeBL
    {
        private readonly RoleeDA _da = new RoleeDA();

        public List<Role> GetAll()
        {
            return _da.GetAll();
        }

        public int Insert(Role r)
        {
            return _da.Insert_Update_Delete(r, 0);
        }

        public int Update(Role r)
        {
            return _da.Insert_Update_Delete(r, 1);
        }

        public int Delete(Role r)
        {
            return _da.Insert_Update_Delete(r, 2);
        }
    }
}
