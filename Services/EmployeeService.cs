using paytrack_api.Models;
using paytrack_api.Services.Interfaces;
using paytrack_api.Repository;
using paytrack_api.Repository.Interfaces;

namespace paytrack_api.Services
{
    public class EmployeeService: IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            this._employeeRepository = employeeRepository;
        }
        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await Task.Run(() => _employeeRepository.GetAll());
        }
        public async Task<Employee> GetById(int id)
        {
            return await Task.Run(() => _employeeRepository.GetById(id));
        }
        public async Task<bool> Add(Employee employee)
        {
            return _employeeRepository.Add(employee);
        }
        public async Task<bool> Update(Employee employee)
        {
            return await Task.Run(() => _employeeRepository.Update(employee));
        }
        public async Task<bool> Delete(Employee employee)
        {
            return await Task.Run(() => _employeeRepository.Delete(employee));
        }

    }
}
