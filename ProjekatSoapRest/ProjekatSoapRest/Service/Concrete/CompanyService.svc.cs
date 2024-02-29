using System;
using System.Collections.Generic;

namespace ProjekatSoapRest
{
    public class CompanyService : ICompanyServiceRest, ICompanyServiceSoap
    {
        // trenutno in-memory baza, to jest samo staticka lista kompanija
        static List<Company> companyList = new List<Company>();
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        private Company addCompany(Company company)
        {
            companyList.Add(company);
            return company;
        }

        private Company getCompanyById(string companyId)
        {
            return companyList.Find(c => c.Id.ToString().Equals(companyId));
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
