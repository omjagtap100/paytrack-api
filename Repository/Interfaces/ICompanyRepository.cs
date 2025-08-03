using paytrack_api.Models;

namespace paytrack_api.Repository.Interfaces
{
    public interface ICompanyRepository:IRepository<Company>
    {
        public Task<IEnumerable<Company>> GetCompanies();
    }
}
