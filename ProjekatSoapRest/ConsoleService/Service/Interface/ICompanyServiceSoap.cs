using System.ServiceModel;
using ProjekatSoapRest.Model;

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
