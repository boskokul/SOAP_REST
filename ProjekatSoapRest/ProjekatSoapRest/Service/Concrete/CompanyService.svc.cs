using ProjekatSoapRest.Service.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel.Web;
using System.Xml.Serialization;

namespace ProjekatSoapRest
{
    public class CompanyService : ICompanyServiceRest, ICompanyServiceSoap, IValidator
    {
        private List<Company> getCompanies()
        {
            var mySerializer = new XmlSerializer(typeof(List<Company>));
            var myFileStream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "/Data/companies.xml", FileMode.Open);
            List<Company> companies = (List<Company>)mySerializer.Deserialize(myFileStream);
            myFileStream.Close();
            return companies;
        }

        private Company addCompany(Company company)
        {
            WebOperationContext ctx = WebOperationContext.Current;
            if (!Validate(company))
            {
                ctx.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return null;
            }
            List<Company> companies = getCompanies();
            companies.Add(company);
            XmlSerializer x = new XmlSerializer(companies.GetType());
            StreamWriter myWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "/Data/companies.xml");
            x.Serialize(myWriter, companies);
            myWriter.Close();
            ctx.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.OK;
            return company;
        }

        private Company getCompanyById(string companyId)
        {
            var company = getCompanies().Find(c => c.Id.ToString().Equals(companyId));
            WebOperationContext ctx = WebOperationContext.Current;
            if(company != null)
            {
                ctx.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.OK;
            }
            else
            {
                ctx.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
            }
            return company;

        }
        public Company AddCompanySoap(Company company)
        {
            return addCompany(company);
        }

        public Company GetCompanyByIdSoap(string companyId)
        {
            return getCompanyById(companyId);
        }

        public Company AddCompanyRest(Company company)
        {
            return addCompany(company);
        }

        public Company GetCompanyByIdRest(string companyId)
        {
            return getCompanyById(companyId);
        }

        public bool Validate(Company company)
        {
            List<Company> companies = getCompanies();
            if (companies.Find(c => c.Id.ToString().Equals(company.Id)) != null)
            {
                return false;
            }
            if (!company.Validate())
            {
                return false;
            }
            if (!validateEmpolyees(company.Employees))
            {
                return false;
            }
            if (!checkUniqueName(company, companies) || !checkUniqueEmployee(company.Employees, companies))
            {
                return false;
            }
                return true;
        }
        private bool validateEmpolyees(List<Employee> employees)
        {
            foreach (Employee employee in employees)
            {
                if (!employee.Validate())
                {
                    return false;
                }
            }
            return true;
        }
        private bool checkUniqueName(Company company, List<Company> companies)
        {
            if (companies.Exists(c => c.Name == company.Name))
            {
                return false;
            }
            return true;
        }
        private bool checkUniqueEmployee(List<Employee> employees, List<Company> companies)
        {
            foreach (Employee employee in employees)
            {
                foreach (Company company in companies)
                {
                    if (company.Employees.Exists(e => e.JMBG == employee.JMBG && !e.FirstName.Equals(employee.FirstName) && !e.LastName.Equals(employee.LastName)))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
