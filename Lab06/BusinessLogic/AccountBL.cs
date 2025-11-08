using System.Collections.Generic;
using DataAccess;
using DataAccess.OL;

namespace BusinessLogic
{
    public class AccountBL
    {
        private readonly AccountDA _da = new AccountDA();

        public List<Account> GetAll()
        {
            return _da.GetAll();
        }

        public int Insert(Account a)
        {
            return _da.Insert_Update_Delete(a, 0);
        }

        public int Update(Account a)
        {
            return _da.Insert_Update_Delete(a, 1);
        }

        public int Delete(Account a)
        {
            return _da.Insert_Update_Delete(a, 2);
        }
    }
}
