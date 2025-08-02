using sample.Models;

namespace sample.Interfaces
{
    public interface ICrudBL<T> where T : class
    {
        bool Add(T t);
        bool Update(T t, int id);
        bool Delete(int id);
        List<T> GetList();
        T GetById(int id);

    }
}
