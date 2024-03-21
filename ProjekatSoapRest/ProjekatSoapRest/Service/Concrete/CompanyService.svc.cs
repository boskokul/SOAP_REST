using ProjekatSoapRest.Service.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace ProjekatSoapRest
{
    public class CompanyService : ICompanyServiceRest, ICompanyServiceSoap, IValidator
    {
        public List<Company> GetCompanies()
        {
            var mySerializer = new XmlSerializer(typeof(List<Company>));
            
            var myFileStream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "/Data/companies.xml", FileMode.Open);
            
            List<Company> companies = (List<Company>)mySerializer.Deserialize(myFileStream);
            myFileStream.Close();
            return companies;
        }

        private Company addCompany(Company company)
        {
            if (!Validate(company))
            {
                return null;
            }
            List<Company> companies = GetCompanies();
            companies.Add(company);
            XmlSerializer x = new XmlSerializer(companies.GetType());
            StreamWriter myWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "/Data/companies.xml");
            x.Serialize(myWriter, companies);
            myWriter.Close();
            return company;
        }

        private Company getCompanyById(string companyId)
        {
            var companies = GetCompanies();
            return companies.Find(c => c.Id.ToString().Equals(companyId));
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
            if(!company.Validate())
            {
                return false;
            }
            if (!validateEmpolyees(company.Employees))
            {
                return false;
            }
            if (!checkUniqueName(company) || !checkUniqueEmployee(company.Employees))
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
        private bool checkUniqueName(Company company)
        {
            List<Company> companies = GetCompanies();
            if (companies.Exists(c => c.Name == company.Name))
            {
                return false;
            }
            return true;
        }

        //malo neefikasno algoritamski
        private bool checkUniqueEmployee(List<Employee> employees)
        {
            List<Company> companies = GetCompanies();
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
