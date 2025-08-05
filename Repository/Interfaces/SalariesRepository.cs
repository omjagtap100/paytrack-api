using paytrack_api.Models;
using System.Data;

namespace paytrack_api.Repository.Interfaces
{
  
        public class SalariesRepository : Repository<Salaries>, ISalariesRepository
        {
            private readonly IDbConnection _connection;
            public SalariesRepository(DapperConnection context) : base(context)
            {
                this._connection = context.CreateConnection();
            }

        }
    }

