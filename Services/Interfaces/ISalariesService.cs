using paytrack_api.Models;
using paytrack_api.Repository.Interfaces;

namespace paytrack_api.Services.Interfaces
{
    public interface ISalariesService : IService<Salaries>
    {
        public  Task<Salaries> GetByEmpId(int empId);
    }
}

