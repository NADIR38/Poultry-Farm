using sample.Models;

namespace sample.Repository
{
    public interface ICustomerRepository
    {
        bool Add(Customers t);
        bool Update(Customers t, int id);
        bool Delete(int id);
        List<Customers> GetAll();
        List<Customers> SearchByName(string name);
        Customers GetById(int id);
    }
}
