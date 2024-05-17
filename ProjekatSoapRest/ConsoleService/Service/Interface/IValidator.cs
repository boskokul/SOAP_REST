using System.Collections.Generic;
using ProjekatSoapRest.Model;

namespace ConsoleService.Service.Interface
{
    public interface IValidator
    {
        bool ValidateCompany(Company company, List<Company> existingCompanies);
    }
}
