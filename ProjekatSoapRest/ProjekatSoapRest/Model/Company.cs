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

        public bool ValidateDepartments()
        {
            HashSet<string> deparmentNames = new HashSet<string>(Departments.ConvertAll(d => d.ToLower()));
            if (deparmentNames.Count != Departments.Count)
            {
                return false;
            }
            return true;
        }

    }
}