using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Runtime.Serialization;

namespace ConsoleService.Model
{
    [DataContract]
    public class Employee
    {
        [DataMember, Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
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
        [DataMember]
        public bool DeservesRaise { get; set; }

        public bool Validate()
        {
            return !(String.IsNullOrEmpty(FirstName) || String.IsNullOrEmpty(LastName) || String.IsNullOrEmpty(Email) || DateOfBirth == default(DateTime));
        }
    }
}
