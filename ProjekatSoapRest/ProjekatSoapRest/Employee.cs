using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ProjekatSoapRest
{
    [DataContract]
    public class Employee
    {
        [DataMember]
        public long JMBG { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember(Name = "DateOfBirth")]
        public string FormattedDateTime
        {
            get => $"{DateOfBirth:yyyy-MM-dd hh:mm:ss}";
            set => DateOfBirth = DateTime.ParseExact(value, "yyyy-MM-dd hh:mm:ss", CultureInfo.CurrentCulture);
        }
        public DateTime DateOfBirth { get; set; }
        public DateTime MyDateTime { get; set; }
        [DataMember]
        public bool DeservesRaise { get; set; }
    }
}