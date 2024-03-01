using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ProjekatSoapRest
{
    public class CompanyService : ICompanyServiceRest, ICompanyServiceSoap
    {
        // trenutno in-memory baza, to jest samo staticka lista kompanija
        static List<Company> companyList = new List<Company>();
        static List<Employee> employeeList = new List<Employee>();

        public List<Company> GetCompanies()
        {
            var mySerializer = new XmlSerializer(typeof(List<Company>));
            // To read the file, create a FileStream.
            var myFileStream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "/Data/companies.xml", FileMode.Open);
            // Call the Deserialize method and cast to the object type.
            List<Company> companies = (List<Company>)mySerializer.Deserialize(myFileStream);
            myFileStream.Close();
            return companies;
        }

        private Company addCompany(Company company)
        {
            if (!checkUniqueName(company))
            {
                return null;
            }
            
            if (!company.ValidateDepartments())
            {
                return null;
            }

            if (!checkUniqueEmployee(company))
            {
                return null;
            }
            companyList = GetCompanies();
            companyList.Add(company);
            employeeList.AddRange(company.Employees);
            System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(companyList.GetType());
            StreamWriter myWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "/Data/companies.xml");
            x.Serialize(myWriter, companyList);
            myWriter.Close();
            return company;
        }
        private bool checkUniqueEmployee(Company company)
        {
            foreach (Employee employee in company.Employees)
            {
                if (employeeList.Exists(e => e.JMBG == employee.JMBG && !e.FirstName.Equals(employee.FirstName) && !e.LastName.Equals(employee.LastName)))
                {
                    return false;
                }
            }
            return true;
        }
        private bool checkUniqueName(Company company)
        {
            if (companyList.Exists(c => c.Name == company.Name))
            {
                return false;
            }
            return true;
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
    }
}
