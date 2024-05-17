using ProjekatSoapRest.Model;
using System.Collections.Generic;

namespace ProjekatSoapRest.Data
{
    public interface ICustomDbContext
    {
        List<Company> GetCompaniesDB();
        void SaveCompaniesDB(Company company);
        Company GetCompanyById(string companyId);
    }
}
