namespace Services.Interfaces
{
    public interface ICRUD<T>
    {
        T Get(int id);
        IEnumerable<T> GetAll(int id = default);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
