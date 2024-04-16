using ProjekatSoapRest.Data;
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
        private string FilePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data/", "companies.xml");

        private List<Company> GetCompanies()
        {
            var serializer = new XmlSerializer(typeof(List<Company>));
                using (var fileStream = new FileStream(FilePath, FileMode.Open))
                {
                    return (List<Company>)serializer.Deserialize(fileStream);
                }
        }

        protected CustomDbContext DbContext;
        private DbSet<Company> CompaniesDBSet;

        private Company AddCompany(Company company)
        {
            DbContext = new CustomDbContext();
            CompaniesDBSet = DbContext.Companies;
            var companies = GetCompanies();
            if (!ValidateCompany(company, companies))
            {
                SetResponseStatus(HttpStatusCode.BadRequest);
                return null;
            }
            companies.Add(company);
            SaveCompanies(companies);
            SetResponseStatus(HttpStatusCode.OK);
            CompaniesDBSet.Add(company);
            DbContext.SaveChanges();
            return company;
        }

        private void SaveCompanies(List<Company> companies)
        {
            var serializer = new XmlSerializer(companies.GetType());
            using (var writer = new StreamWriter(FilePath))
            {
                serializer.Serialize(writer, companies);
            }
        }

        private void SetResponseStatus(HttpStatusCode statusCode)
        {
            WebOperationContext.Current.OutgoingResponse.StatusCode = statusCode;
        }

        private Company GetCompanyById(string companyId)
        {
            var company = GetCompanies().Find(c => c.Id.ToString().Equals(companyId));
            WebOperationContext ctx = WebOperationContext.Current;
            if(company != null)
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
            return ValidateCompany(company, GetCompanies());
        }

        private bool ValidateCompany(Company company, List<Company> existingCompanies)
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
    }
}
