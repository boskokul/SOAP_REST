using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ProjekatSoapRest.Model
{
    [DataContract]
    public class Department
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [DataMember]
        public string Name { get; set; }

        Department() { }

        Department(long id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}