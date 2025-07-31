using paytrack_api.Models;
using System.Collections.Generic;

namespace paytrack_api.Services.Interfaces
{
    public interface ICompanyService
    {
        public  Task<IEnumerable<Company>> GetCompanyData();


    }
}
