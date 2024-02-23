﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

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

        public Company AddCompany(Company company)
        {
            companyList.Add(company);
            return company;
        }

        public Company GetCompanyById(string companyId)
        {
            return companyList.Find(c => c.Id.ToString().Equals(companyId));
        }
    }
}
