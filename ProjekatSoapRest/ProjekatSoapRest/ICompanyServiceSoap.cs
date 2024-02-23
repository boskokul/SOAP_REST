using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

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
