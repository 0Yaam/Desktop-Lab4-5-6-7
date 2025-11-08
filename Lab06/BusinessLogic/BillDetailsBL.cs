using DataAccess;
using DataAccess.OL;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    public class BillDetailsBL
    {
        private readonly BillDetailsDA _da = new BillDetailsDA();

        public List<BillDetails> GetAll()
        {
            return _da.GetAll();
        }

        public int Insert(BillDetails d)
        {
            return _da.Insert_Update_Delete(d, 0);
        }

        public int Update(BillDetails d)
        {
            return _da.Insert_Update_Delete(d, 1);
        }

        public int Delete(BillDetails d)
        {
            return _da.Insert_Update_Delete(d, 2);
        }
        public List<BillDetails> GetByBill(int billId)
        {
            return _da.GetAll().Where(d => d.InvoiceID == billId).ToList();
        }
    }
}
