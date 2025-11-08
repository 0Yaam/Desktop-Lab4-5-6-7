using System;

namespace _2312590_NNTDan_Lab07.Models
{
    public class Bill
    {
        public int Id
        {
            get; set;
        }
        public int TableId
        {
            get; set;
        }
        public DateTime CheckIn
        {
            get; set;
        }
        public DateTime? CheckOut
        {
            get; set;
        }
        public int DiscountPercent
        {
            get; set;
        }
        public bool IsPaid
        {
            get; set;
        }
        public int? StaffId
        {
            get; set;
        }
        public string Notes
        {
            get; set;
        }

        public virtual DiningTable Table
        {
            get; set;
        }
        public virtual Account Staff
        {
            get; set;
        }
        public virtual System.Collections.Generic.ICollection<BillDetail> Details
        {
            get; set;
        }
    }
}
