using ProjekatSoapRest.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ProjekatSoapRest
{
    [DataContract]
    public class Company
    {
        [DataMember, Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public List<Department> Departments { get; set; }
        [DataMember]
        public List<Employee> Employees { get; set; }

        public bool Validate()
        {
            if (String.IsNullOrEmpty(Name) || Departments == null || Employees == null || Departments.Count == 0 || Employees.Count == 0 || !ValidateDepartments())
              {
                  return false;
              }
              return true;
        }

        public bool ValidateDepartments()
        {
            HashSet<string> uniqueDepartmentNames = new HashSet<string>(Departments.ConvertAll(d => d.Name.ToLower()));
            return uniqueDepartmentNames.Count == Departments.Count;
        }
    }
}