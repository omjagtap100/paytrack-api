namespace paytrack_api.Utilities
{
    public class SalaryCalculationUtility
    {
        public static long CalculateDeductions(Salaries salaries)
        {
            return salaries.PF;
        }

        public static long calculateNetSalary(Salaries salaries)
        {
            long totalEarnings = salaries.BasicSalary + salaries.HRA + salaries.DA + salaries.PF;
            long totalDeductions = CalculateDeductions(salaries);
            return totalEarnings - totalDeductions;
        }


        public static long calculateGrossSalary(Salaries salaries)
        {
              return salaries.BasicSalary + salaries.HRA + salaries.DA + salaries.PF;
            
        }
    }
}
