using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poultary.BL.Models
{
    public class Supplier
    {
        public int SupplierID { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public string SupplierType { get; set; }

        public Supplier() { }
        public Supplier(string name, string contact, string address,string supplierType)
        {
            Name = name;
            Contact = contact;
            Address = address;
            this.SupplierType = supplierType;
        }
    }
}
