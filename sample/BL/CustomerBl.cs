using pro.DL;
using sample.Interfaces;
using sample.Models;

namespace sample.BL
{
    public class CustomerBl : ICrudBL<Customers>
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

        public bool Delete(int id)
        {
            return CustomerDL.DeleteCustomer(id);
        }

        public List<Customers> GetList()
        {
            return CustomerDL.GetCustomers();
        }

        public List<Customers> GetListByName(string name)
        {
            return CustomerDL.SearchCustomersByName(name);
        }

    }
}
