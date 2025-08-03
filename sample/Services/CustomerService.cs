using pro.DL;
using sample.Interfaces;
using sample.Models;
using sample.Repository;

namespace sample.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepostory;

        public CustomerService(ICustomerRepository customerRepostory)
        {
            _customerRepostory = customerRepostory;
        }

        public bool Add(Customers t)
        {
            if (string.IsNullOrWhiteSpace(t.Name)) return false;
            if (string.IsNullOrWhiteSpace(t.Contact)) return false;
            if (string.IsNullOrWhiteSpace(t.Address)) return false;

            return _customerRepostory.Add(t);
        }

        public bool Update(Customers t, int id)
        {
            if (string.IsNullOrWhiteSpace(t.Name)) return false;
            if (string.IsNullOrWhiteSpace(t.Contact)) return false;
            if (string.IsNullOrWhiteSpace(t.Address)) return false;

            return _customerRepostory.Update(t, id);
        }

        public bool Delete(int id)
        {
            if (id <= 0) return false;

            return _customerRepostory.Delete(id);
        }

        public List<Customers> GetAll()
        {
            return _customerRepostory.GetAll();
        }

        public List<Customers> SearchByName(string name)
        {
            return _customerRepostory.SearchByName(name);
        }

        public Customers GetById(int id)
        {
            if (id <= 0) return null;

            return _customerRepostory.GetById(id);
        }

    }
}
