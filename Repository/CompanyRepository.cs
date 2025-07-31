using Dapper;
using paytrack_api.Models;
using paytrack_api.Repository.Interfaces;
using System.Data;

namespace paytrack_api.Repository
{
    public class CompanyRepository: ICompanyRepository
    {
        private readonly IDbConnection _connection;
        public CompanyRepository(DapperConnection context)
        {
            this._connection = context.CreateConnection();
        }
        public async Task<IEnumerable<Company>> GetCompanies()
        {
            var query = "SELECT * FROM Company";
            var companies = await _connection.QueryAsync<Company>(query);
            _connection.Close();
            return companies.ToList();           
        }
    }
}


