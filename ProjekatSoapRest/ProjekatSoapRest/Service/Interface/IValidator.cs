using System.Collections.Generic;

namespace ProjekatSoapRest.Service.Interface
{
    public interface IValidator
    {
        bool ValidateCompany(Company company, List<Company> existingCompanies);
    }
}
