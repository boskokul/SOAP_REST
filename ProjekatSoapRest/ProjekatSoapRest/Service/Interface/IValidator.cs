using System.Collections.Generic;
using ProjekatSoapRest.Model;

namespace ProjekatSoapRest.Service.Interface
{
    public interface IValidator
    {
        bool ValidateCompany(Company company, List<Company> existingCompanies);
    }
}
