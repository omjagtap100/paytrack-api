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

        public async Task<Salaries> GetById(int id)
        {
            return await Task.Run(() => _salariesRepository.GetById(id));
        }

        public async Task<bool> Add(Salaries salary)
        {
            return _salariesRepository.Add(salary);
        }

        public async Task<bool> Update(Salaries salary)
        {
            return await Task.Run(() => _salariesRepository.Update(salary));
        }

        public async Task<bool> Delete(Salaries salary)
        {
            return await Task.Run(() => _salariesRepository.Delete(salary));
        }
    }
}
