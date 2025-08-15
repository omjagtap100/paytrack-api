using Dapper;
using paytrack_api.Models;
using paytrack_api.Repository.Interfaces;
using System.Data;

namespace paytrack_api.Repository
{

    public class HandleQueriesClientRepository : Repository<HandleQueriesClient>, IHandleQueriesClientRepository
    {
        private readonly IDbConnection _connection;
        public HandleQueriesClientRepository(DapperConnection context) : base(context)
        {
            _connection = context.CreateConnection();
        }


    }
}

