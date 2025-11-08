namespace _2312590_NNTDan_Lab07.Models
{
    public class BillDetail
    {
        public int Id
        {
            get; set;
        }
        public int BillId
        {
            get; set;
        }
        public int FoodId
        {
            get; set;
        }
        public int Quantity
        {
            get; set;
        }
        public int UnitPrice
        {
            get; set;
        }
        public string Notes
        {
            get; set;
        }

        public virtual Bill Bill
        {
            get; set;
        }
        public virtual Food Food
        {
            get; set;
        }
    }
}
