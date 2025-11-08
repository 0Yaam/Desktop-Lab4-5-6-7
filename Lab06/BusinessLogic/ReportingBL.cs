using DataAccess;
using DataAccess.OL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    public class BillView
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int TableID { get; set; }
        public decimal Amount { get; set; }     // NET
        public decimal Discount { get; set; }   // money
        public decimal Tax { get; set; }        // money
        public bool Status { get; set; }
        public DateTime? CheckoutDate { get; set; }
        public string Account { get; set; }
    }

    public class RevenueTotals
    {
        public decimal TotalGross { get; set; }    // u?c tính: net + discount - tax
        public decimal TotalDiscount { get; set; } // sum(Discount)
        public decimal TotalNet { get; set; }      // sum(Amount)
    }

    public class ReportingBL
    {
        private readonly ReportingDA _reportDA = new ReportingDA();
        private readonly BillsBL _billsBL = new BillsBL();

        public List<BillView> GetPaidBills(DateTime? fromDate, DateTime? toDateExclusive)
        {
            var all = _billsBL.GetAll()
                               .Where(b => b.Status) // dã thanh toán
                               .AsEnumerable();

            if (fromDate.HasValue)
                all = all.Where(b => b.CheckoutDate.HasValue && b.CheckoutDate.Value >= fromDate.Value);

            if (toDateExclusive.HasValue)
                all = all.Where(b => b.CheckoutDate.HasValue && b.CheckoutDate.Value < toDateExclusive.Value);

            return all.Select(b => new BillView
            {
                ID = b.ID,
                Name = b.Name,
                TableID = b.TableID,
                Amount = SafeDec(b.Amount),
                Discount = SafeDec(b.Discount),
                Tax = SafeDec(b.Tax),
                Status = b.Status,
                CheckoutDate = b.CheckoutDate,
                Account = b.Account
            }).ToList();
        }

        public RevenueTotals ComputeTotals(IEnumerable<BillView> bills)
        {
            decimal totalNet = 0, totalDiscount = 0, totalGross = 0;
            foreach (var b in bills)
            {
                totalNet += b.Amount;
                totalDiscount += b.Discount;

                // suy ngu?c gross ~ amount + discount - tax
                var gross = b.Amount + b.Discount - b.Tax;
                if (gross < 0) gross = 0;
                totalGross += gross;
            }
            return new RevenueTotals
            {
                TotalGross = totalGross,
                TotalDiscount = totalDiscount,
                TotalNet = totalNet
            };
        }
        public List<OrderLineView> GetOrderLines(int billId)
        {
            var raws = _reportDA.GetOrderLines(billId);
            return raws.Select(x => new OrderLineView
            {
                FoodName = x.FoodName,
                Unit = x.Unit,
                UnitPrice = x.UnitPrice,
                Quantity = x.Quantity,
                LineTotal = x.LineTotal
            }).ToList();
        }
        private static decimal SafeDec(int v) => Convert.ToDecimal(v);
        private static decimal SafeDec(double? v) => Convert.ToDecimal(v ?? 0.0);
    }
}