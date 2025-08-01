using sample.Models;

namespace sample.Interfaces
{
    public interface ICrudDL<T> where T : class
    {
        bool Add(T t);
        bool Update(T t, int id);
        bool Delete(int id);
        List<T> GetAllList();
    }
}
