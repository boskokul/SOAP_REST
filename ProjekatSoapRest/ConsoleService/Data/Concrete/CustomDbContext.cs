using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ConsoleService.Model;

namespace ConsoleService.Data
{
    public class CustomDbContext : DbContext, ICustomDbContext
    {
        public DbSet<Company> CompaniesDBSet { get; set; }
        public DbSet<Employee> EmployeeDBSet { get; set; }
        public DbSet<Department> DepartmentsDBSet { get; set; }

        public CustomDbContext() : base("name=MyPostgresConnection")
        {
        }
        public List<Company> GetCompaniesDB()
        {
            return CompaniesDBSet.Include(c => c.Departments).Include(c => c.Employees).ToList();
        }
        public void SaveCompaniesDB(Company company)
        {
            var companyOld = CompaniesDBSet.Include(c => c.Departments).Include(c => c.Employees).SingleOrDefault(c => c.Id == company.Id);
            if (companyOld != null)
            {
                UpdateCompany(companyOld, company);
            }
            else
            {
                CompaniesDBSet.Add(company);
            }
            SaveChanges();
        }
        private void UpdateCompany(Company companyOld, Company company)
        {
            companyOld.Name = company.Name;
            EmployeeDBSet.RemoveRange(companyOld.Employees);
            companyOld.Employees.Clear();
            companyOld.Employees.AddRange(company.Employees);
            DepartmentsDBSet.RemoveRange(companyOld.Departments);
            companyOld.Departments.Clear();
            companyOld.Departments.AddRange(company.Departments);
        }

        public Company GetCompanyById(string companyId)
        {
            return CompaniesDBSet.Include(c => c.Departments).Include(c => c.Employees).FirstOrDefault(c => c.Id.ToString() == companyId);
        }
    }
}
