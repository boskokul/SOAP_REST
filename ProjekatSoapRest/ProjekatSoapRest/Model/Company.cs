using System;
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


        public bool Validate()
        {
            if (String.IsNullOrEmpty(Name) || Departments == null ||  Employees == null)
            {
                return false;
            }
            else if(Departments.Count == 0 || Employees.Count == 0 || !ValidateDepartments())
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool ValidateDepartments()
        {
            HashSet<string> uniqueDeparmentNames = new HashSet<string>(Departments.ConvertAll(d => d.ToLower()));
            if (uniqueDeparmentNames.Count != Departments.Count)
            {
                return false;
            }
            return true;
        }

    }
}