namespace sample.Models
{
    public class CustomerPayments
    {
        public int PaymentId { get; set; }
        public int CustomerId { get; set; }
        public Customers Customers { get; set; }
        public int BillId { get; set; }
        public CustomerBills CustomerBills { get; set; }
        public decimal PayedAmount { get; set; }
        public decimal DueAmount { get; set; }
        public string Notes { get; set; }
    }
}
