using System.Collections.Generic;
using ConsoleService.Model;

namespace ConsoleService.Service.Interface
{
    public interface IValidator
    {
        bool ValidateCompany(Company company, List<Company> existingCompanies);
    }
}
