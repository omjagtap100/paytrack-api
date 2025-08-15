using paytrack_api.Models;
using paytrack_api.Services.Interfaces;
using paytrack_api.Repository.Interfaces;

namespace paytrack_api.Services
{
    public class HandleQueriesClientService : IHandleQueriesClientService
    {
        private readonly IHandleQueriesEmployeesRepository _HandleQueriesEmployeeRepository;

        public HandleQueriesClientService(IHandleQueriesEmployeesRepository HandleQueriesEmployeeRepository)
        {
            this._HandleQueriesEmployeeRepository = HandleQueriesEmployeeRepository;
        }

        public async Task<IEnumerable<HandleQueriesEmployee>> GetAll()
        {
            return await Task.Run(() => _HandleQueriesEmployeeRepository.GetAll());
        }

 



        public async Task<bool> Delete(HandleQueriesEmployee salary)
        {
            return await Task.Run(() => _HandleQueriesEmployeeRepository.Delete(salary));
        }

        public Task<HandleQueriesEmployee> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Add(HandleQueriesEmployee entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(HandleQueriesEmployee entity)
        {
            throw new NotImplementedException();
        }

        Task<HandleQueriesClient> IService<HandleQueriesClient>.GetById(int id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<HandleQueriesClient>> IService<HandleQueriesClient>.GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Add(HandleQueriesClient entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(HandleQueriesClient entity)
        {
            throw new NotImplementedException();
        }


    }

}
