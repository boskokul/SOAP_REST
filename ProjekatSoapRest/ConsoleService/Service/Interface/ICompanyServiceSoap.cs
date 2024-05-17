using System.ServiceModel;
using ConsoleService.Model;

namespace ConsoleService.Service.Interface
{
    [ServiceContract]
    public interface ICompanyServiceSoap
    {
        [OperationContract]
        Company AddCompanySoap(Company company);

        [OperationContract]
        Company GetCompanyByIdSoap(string companyId);
    }
}
