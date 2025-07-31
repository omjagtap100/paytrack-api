using paytrack_api.Models;

namespace paytrack_api.Repository.Interfaces
{
    public interface ICompanyRepository
    {
        public Task<IEnumerable<Company>> GetCompanies();
    }
}
