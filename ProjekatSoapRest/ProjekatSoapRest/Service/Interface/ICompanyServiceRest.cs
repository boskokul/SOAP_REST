using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace ProjekatSoapRest
{
    [ServiceContract]
    public interface ICompanyServiceRest
    {
        // post http://localhost:52336/Concrete/CompanyService.svc/rest/Company
        // primer jsona za ovaj post: {"Id":"5","Name":"PepsiCo", "Departments":["Tehnoloski", "Prodajni"], "Employees":[{"JMBG":"12", "FirstName":"Marko", "LastName":"Markovic", "Email":"markoni@gmail.com", "DeservesRaise":false, "DateOfBirth":"2017-09-08 11:20:12"}]}
        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "/Company",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        Company AddCompanyRest(Company company);


        // get http://localhost:52336/Concrete/CompanyService.svc/rest/Company/5
        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "/Company/{companyId}",
            BodyStyle = WebMessageBodyStyle.Wrapped,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        Company GetCompanyByIdRest(string companyId);
    }

}
