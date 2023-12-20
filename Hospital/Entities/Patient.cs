using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Entities
{
    [Table("Patients")]
    public class Patient
    {
        public Guid? Id { get; set; }
        public string IdentityNo { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
       // public string Role { get; set; } = "hasta";
    }
}
