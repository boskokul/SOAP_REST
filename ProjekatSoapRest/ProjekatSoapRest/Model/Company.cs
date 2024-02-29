using System.Collections.Generic;
using System.Runtime.Serialization;

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