namespace paytrack_api.Services.Interfaces
{
    public interface IService<T> where T:class
    {

            Task<T> GetById(int id);
            Task<IEnumerable<T>> GetAll();
            Task<bool> Add(T entity);
            Task<bool> Update(T entity);
            Task<bool> Delete(T entity);
    }
}
