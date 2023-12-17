using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Entities
{
    [Table("Doctors")]
    public class Doctor:User
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Department { get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}
