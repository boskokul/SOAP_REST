using ProjekatSoapRest.Data;
using ProjekatSoapRest.Model;
using ProjekatSoapRest.Service.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel.Web;
using System.Xml.Serialization;

namespace ProjekatSoapRest
{
    public class CompanyService : ICompanyServiceRest, ICompanyServiceSoap, IValidator
    {
        private CustomDbContext DbContext;
        private DbSet<Company> CompaniesDBSet;
        private DbSet<Employee> EmployeeDBSet;
        private DbSet<Department> DepartmentsDBSet;

        public CompanyService() {
            DbContext = new CustomDbContext();
            CompaniesDBSet = DbContext.Companies;
            EmployeeDBSet = DbContext.Employees;
            DepartmentsDBSet = DbContext.Departments;
        }

        private List<Company> GetCompaniesDB()
        {
            return CompaniesDBSet.Include(c => c.Departments).Include(c => c.Employees).ToList();
        }

        private void SaveCompaniesDB(Company company)
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
            DbContext.SaveChanges();
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

        private Company AddCompany(Company company)
        {
            var companies = GetCompaniesDB();
            companies.RemoveAll(c => c.Id == company.Id);
            if (!ValidateCompany(company, companies))
            {
                SetResponseStatus(HttpStatusCode.BadRequest);
                return null;
            }
            SaveCompaniesDB(company);
            SetResponseStatus(HttpStatusCode.OK);
            return company;
        }

        private void SetResponseStatus(HttpStatusCode statusCode)
        {
            WebOperationContext.Current.OutgoingResponse.StatusCode = statusCode;
        }

        private Company GetCompanyById(string companyId)
        {
            WebOperationContext ctx = WebOperationContext.Current;
            var company = CompaniesDBSet.Include(c => c.Departments).Include(c => c.Employees).FirstOrDefault(c => c.Id.ToString() == companyId);
            if (company != null)
            {
                ctx.OutgoingResponse.StatusCode = HttpStatusCode.OK;
            }
            else
            {
                ctx.OutgoingResponse.StatusCode = HttpStatusCode.NotFound;
            }
            return company;
        }

        public Company AddCompanySoap(Company company)
        {
            return AddCompany(company);
        }

        public Company GetCompanyByIdSoap(string companyId)
        {
            return GetCompanyById(companyId);
        }

        public Company AddCompanyRest(Company company)
        {
            return AddCompany(company);
        }

        public Company GetCompanyByIdRest(string companyId)
        {
            return GetCompanyById(companyId);
        }

        public bool Validate(Company company)
        {
            return ValidateCompany(company, GetCompaniesDB());
        }

        public bool ValidateCompany(Company company, List<Company> existingCompanies)
        {
            if (existingCompanies.Any(c => c.Id.ToString().Equals(company.Id)))
            {
                return false;
            }
            if (!company.Validate() || !ValidateEmpolyees(company.Employees) || !IsUniqueName(company, existingCompanies) || !IsUniqueEmployee(company.Employees,existingCompanies))
            {
                return false;
            }
            return true;
        }

        private bool ValidateEmpolyees(List<Employee> employees)
        {
            return employees.All(employee => employee.Validate());
        }

        private bool IsUniqueName(Company company, List<Company> existingCompanies)
        {
        	return !existingCompanies.Exists(c => c.Name == company.Name);
        }

        private bool IsUniqueEmployee(List<Employee> employees, List<Company> existingCompanies)
        {
        	return employees.All(employee => existingCompanies.All(c => c.Employees.All(e => e.JMBG != employee.JMBG && (e.FirstName != employee.FirstName || e.LastName != employee.LastName))));
        }

        //xml file database
        private string FilePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data/", "companies.xml");

        private List<Company> GetCompaniesXML()
        {
            var serializer = new XmlSerializer(typeof(List<Company>));
            using (var fileStream = new FileStream(FilePath, FileMode.Open))
            {
                return (List<Company>)serializer.Deserialize(fileStream);
            }
        }

        private void SaveCompaniesXML(List<Company> companies)
        {
            var serializer = new XmlSerializer(companies.GetType());
            using (var writer = new StreamWriter(FilePath))
            {
                serializer.Serialize(writer, companies);
            }
        }
    }
}
