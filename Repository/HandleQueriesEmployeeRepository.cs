using Dapper;
using paytrack_api.Models;
using paytrack_api.Repository.Interfaces;
using System.Data;

namespace paytrack_api.Repository
{

    public class HandleQueriesEmployeeRepository : Repository<HandleQueriesEmployee>, IHandleQueriesEmployeesRepository
    {
        private readonly IDbConnection _connection;
        public HandleQueriesEmployeeRepository(DapperConnection context) : base(context)
        {
            _connection = context.CreateConnection();
        }


    }
}

