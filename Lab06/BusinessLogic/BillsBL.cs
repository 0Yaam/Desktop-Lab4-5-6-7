using DataAccess;
using DataAccess.OL;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    public class BillsBL
    {
        private readonly BillsDA _da = new BillsDA();
        public Bills GetById(int id) => _da.GetAll().FirstOrDefault(b => b.ID == id);

        public List<Bills> GetAll()
        {
            return _da.GetAll();
        }

        public int Insert(Bills b)
        {
            return _da.Insert_Update_Delete(b, 0);
        }

        public int Update(Bills b)
        {
            return _da.Insert_Update_Delete(b, 1);
        }

        public int Delete(Bills b)
        {
            return _da.Insert_Update_Delete(b, 2);
        }
        public List<Bills> GetOpenByTable(int tableId)
        {
            return _da.GetAll()
                      .Where(b => b.TableID == tableId && b.Status == false)
                      .ToList();
        }
    }
}
