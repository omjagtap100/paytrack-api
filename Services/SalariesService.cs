using paytrack_api.Models;
using paytrack_api.Services.Interfaces;
using paytrack_api.Repository.Interfaces;

namespace paytrack_api.Services
{
    public class SalariesService : ISalariesService
    {
        private readonly ISalariesRepository _salariesRepository;

        public SalariesService(ISalariesRepository salariesRepository)
        {
            this._salariesRepository = salariesRepository;
        }

        public async Task<IEnumerable<Salaries>> GetAll()
        {
            return await Task.Run(() => _salariesRepository.GetAll());
        }

        public async Task<Salaries> GetById(int empId)
        {
            return await Task.Run(() => _salariesRepository.GetByEmpId(empId));
        }

        public async Task<bool> Add(Salaries salary)
        {
            bool isSalaryCorrect = verifySalary(salary);
            if (isSalaryCorrect)
            {
                return await Task.Run(() => _salariesRepository.Add(salary));
            }
            return isSalaryCorrect;
        }

        public async Task<bool> Update(Salaries salary)
        {
            bool isSalaryCorrect = verifySalary(salary);
            if (isSalaryCorrect)
            {
                return await Task.Run(() => _salariesRepository.Update(salary));
            }
            return isSalaryCorrect;
        }

        public async Task<bool> Delete(Salaries salary)
        {
            return await Task.Run(() => _salariesRepository.Delete(salary));
        }
        public async Task<Salaries> GetByEmpId(int empId)
        {
            return await Task.Run(() => _salariesRepository.GetByEmpId(empId));
        }
        public bool verifySalary(Salaries salary)
        {

            Salaries previousSal = _salariesRepository.GetByEmpId(salary.EmployeeId);
            decimal calculatedAmount = salary.BasicSalary + salary.PF + salary.DA + salary.HRA + salary.Deductions;
            if (salary.SalaryAmount != calculatedAmount)
            {
                return false;
            }
            if (previousSal != null)
            {
                previousSal.IsPrevious = true;
                _salariesRepository.Update(previousSal);
            }
            return true;

        }
    }

}
