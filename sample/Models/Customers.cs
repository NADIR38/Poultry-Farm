using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sample.Models
{
    public class Customers
    {
        
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        List<CustomerBills> Bills { get; set; }
        List<CustomerPriceRecord> PriceRecords { get; set; }
        List<CustomerPayments> Payments { get; set; }
    }
}
