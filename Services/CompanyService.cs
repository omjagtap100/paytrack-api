using paytrack_api.Models;
using paytrack_api.Repository.Interfaces;
using paytrack_api.Services.Interfaces;

namespace paytrack_api.Services
{
    public class CompanyService: ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        public CompanyService(ICompanyRepository companyRepository) 
        {
            this._companyRepository = companyRepository;
        }
        public async Task<IEnumerable<Company>> GetCompanyData() {
           return  await _companyRepository.GetCompanies();
        }
        public async Task<Company> GetById(int id)
        {
            var company = _companyRepository.GetById(id);
            return company;
        }

    }
}
