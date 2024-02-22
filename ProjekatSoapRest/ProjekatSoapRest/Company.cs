using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ProjekatSoapRest
{
    [DataContract]
    public class Company
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public List<string> Departments { get; set; }
        [DataMember]
        public List<Employee> Employees { get; set; }

       
    }
}