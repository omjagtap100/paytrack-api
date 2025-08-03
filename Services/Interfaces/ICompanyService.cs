using paytrack_api.Models;
using paytrack_api.Repository.Interfaces;
using System.Collections.Generic;

namespace paytrack_api.Services.Interfaces
{
    public interface ICompanyService
    {
        public  Task<IEnumerable<Company>> GetCompanyData();
        public  Task<Company> GetById(int id);


    }
}
