using System.ServiceModel;

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
