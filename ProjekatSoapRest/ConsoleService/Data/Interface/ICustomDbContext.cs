using ProjekatSoapRest.Model;
using System.Collections.Generic;

namespace ConsoleService.Data
{
    public interface ICustomDbContext
    {
        List<Company> GetCompaniesDB();
        void SaveCompaniesDB(Company company);
        Company GetCompanyById(string companyId);
    }
}
