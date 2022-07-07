namespace Infrastructure.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> ReadAll();
        IEnumerable<T> ReadAll(int id);
        bool Empty(int id);
        T Read(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
