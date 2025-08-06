namespace paytrack_api.Repository.Interfaces
{
    public interface ISalariesRepository : IRepository<Salaries> {

        public Salaries GetByEmpId(int empId);

    }
}
