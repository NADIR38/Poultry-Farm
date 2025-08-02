namespace sample.Models
{
    public class CustomerPriceRecord
    {
        public int RecordId { get; set; }
        public int CustomerId { get; set; }
        public Customers Customers { get; set; }
        public DateTime Date {  get; set; }
        public decimal Payment {  get; set; }
        public int BillId { get; set; }
        public CustomerBills CustomerBills { get; set; }

    }
}
