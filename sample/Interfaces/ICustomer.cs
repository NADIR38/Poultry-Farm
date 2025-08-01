using sample.Models;

namespace sample.Interfaces
{
    public interface ICustomer
    {
        bool Add(Customers s);
        bool Update(Customers s, int id);
        bool delete(int id);
        List<Customers> GetCustomers();

        List<Customers> GetCustomersbyName(string text);
    }
}
