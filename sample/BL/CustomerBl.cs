using pro.DL;
using sample.Interfaces;
using sample.Models;

namespace sample.BL
{
    public class CustomerBl : ICrudBL<Customers>
    {
        private readonly ICrudDL<Customers> _customerDL;

        public CustomerBl(ICrudDL<Customers> customerDL)
        {
            _customerDL = customerDL;
        }

        public bool Add(Customers t)
        {
            if (string.IsNullOrWhiteSpace(t.Name)) return false;
            if (string.IsNullOrWhiteSpace(t.Contact)) return false;
            if (string.IsNullOrWhiteSpace(t.Address)) return false;

            return _customerDL.Add(t);
        }

        public bool Update(Customers t, int id)
        {
            if (string.IsNullOrWhiteSpace(t.Name)) return false;
            if (string.IsNullOrWhiteSpace(t.Contact)) return false;
            if (string.IsNullOrWhiteSpace(t.Address)) return false;

            return _customerDL.Update(t, id);
        }

        public bool Delete(int id)
        {
            if (id <= 0) return false;

            return _customerDL.Delete(id);
        }

        public List<Customers> GetList()
        {
            return _customerDL.GetAllList();
        }

        public List<Customers> GetListByName(string name)
        {
            return CustomerDL.SearchCustomersByName(name);
        }

        public Customers GetById(int id)
        {
            if (id <= 0) return null;

            return _customerDL.GetById(id);
        }
    }
}
