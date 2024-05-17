using System.ServiceModel;
using System.ServiceModel.Web;
using ConsoleService.Model;

namespace ConsoleService
{
    [ServiceContract]
    public interface ICompanyServiceRest
    {
        // post http://localhost:8733/rest/Company
        // primer jsona za ovaj post: {"Id":"187","Name":"PepsiCo", "Departments":[{"Name":"Tehnoloski"}, {"Name":"Prodajni"}], "Employees":[{"JMBG":"12", "FirstName":"Marko", "LastName":"Markovic", "Email":"markoni@gmail.com", "DeservesRaise":false, "DateOfBirth":"2017-09-08 11:20:12"}]}
        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "/Company",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        Company AddCompanyRest(Company company);


        // get http://localhost:8733/rest/Company/5
        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "/Company/{companyId}",
            BodyStyle = WebMessageBodyStyle.Wrapped,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        Company GetCompanyByIdRest(string companyId);
    }
}
