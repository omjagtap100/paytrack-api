using Dapper;
using paytrack_api.Models;
using paytrack_api.Repository.Interfaces;
using System.Data;

namespace paytrack_api.Repository
{
  
        public class SalariesRepository : Repository<Salaries>, ISalariesRepository
        {
            private readonly IDbConnection _connection;
            public SalariesRepository(DapperConnection context) : base(context)
            {
                _connection = context.CreateConnection();
            }
        public Salaries GetByEmpId(int empId)
        {
            try
            {
                string query = $"SELECT * FROM {GetTableName()} WHERE employeeId = @EmpId AND isPrevious = 0";

               Salaries result =  _connection.Query<Salaries>(query, new { EmpId = empId }).FirstOrDefault();
            return result;
            }
            catch (Exception ex) 
            { 
                return null; 
            }

        }

    }
    }

