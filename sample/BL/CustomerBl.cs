using pro.DL;
using sample.Models;

namespace sample.BL
{
    public class CustomerBl
    {
        public bool Add(Customers s)
        {
            if (string.IsNullOrWhiteSpace(s.Name)) return false;
            if (string.IsNullOrWhiteSpace(s.Contact)) return false;
            if (string.IsNullOrWhiteSpace(s.Address)) return false;

            return CustomerDL.AddCustomer(s);
        }

        public bool Update(Customers s, int id)
        {
            if (string.IsNullOrWhiteSpace(s.Name)) return false;
            if (string.IsNullOrWhiteSpace(s.Contact)) return false;
            if (string.IsNullOrWhiteSpace(s.Address)) return false;

            return CustomerDL.UpdateCustomers(s, id);
        }

        public bool delete(int id)
        {
            return CustomerDL.DeleteCustomer(id);
        }

        public List<Customers> GetCustomers()
        {
            return CustomerDL.GetCustomers();
        }

        public List<Customers> GetCustomersbyName(string name)
        {
            return CustomerDL.SearchCustomersByName(name);
        }
    }
}
