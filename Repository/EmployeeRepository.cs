using paytrack_api.Models;
using paytrack_api.Repository.Interfaces;
using System.Data;

namespace paytrack_api.Repository
{
    public class EmployeeRepository: Repository<Employee>,IEmployeeRepository
    {
        private readonly IDbConnection _connection;
        public EmployeeRepository(DapperConnection context) : base(context)
        {
            this._connection = context.CreateConnection();
        }

    }
}
