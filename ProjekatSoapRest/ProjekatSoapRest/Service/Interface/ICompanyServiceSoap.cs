using System.ServiceModel;
using ProjekatSoapRest.Model;

namespace ProjekatSoapRest
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
