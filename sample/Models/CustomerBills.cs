using System.Data;

namespace sample.Models
{
    public class CustomerBills
    {
        public int BillId{ get; set; }
        public int CustomerId { get; set; }
        public Customers Customers { get; set; }
        public DateTime  SaleDate { get; set; }
        public decimal Weight { get; set; }
        public decimal TotalAmount { get; set; }
        public string Notes { get; set; }
        public List<CustomerPayments> Payments { get; set; }
        public List<CustomerPriceRecord> PriceRecords { get; set; }
    }
}
