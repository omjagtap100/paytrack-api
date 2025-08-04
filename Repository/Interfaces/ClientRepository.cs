using paytrack_api.Repository.Interfaces;
using System.Data;


namespace paytrack_api.Repository
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        private readonly IDbConnection _connection;

        public ClientRepository(DapperConnection context) : base(context)
        {
            _connection = context.CreateConnection();
        }
    }
}