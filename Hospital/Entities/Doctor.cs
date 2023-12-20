using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Entities
{
    [Table("Doctors")]
    public class Doctor
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public int? DepartmentId { get; set; } // Department'a referans oluşturmak için

        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }
     
        public List<Appointment> Appointments { get; set; }
    }
}
