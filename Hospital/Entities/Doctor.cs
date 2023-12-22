using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Entities
{
    [Table("Doctors")]
    public class Doctor
    {
        [Key]
        public  Guid DoctorId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        //doctor girişi için


        [Required]
        [StringLength(30)]
        public string Username { get; set; }

        [Required]
        [StringLength(30)]
        public string Password { get; set; }
        public bool Locked { get; set; }



       
        public  Guid DepartmentId { get; set; } // Doctor sınıfında eklenen DepartmentId özelliği
        public Department Department { get; set; } // Doctor sınıfında eklenen Department özelliği

        public DayOfWeek CalismaGunu { get; set; }
        public TimeSpan BaslangicSaati { get; set; }
        public TimeSpan BitisSaati { get; set; }

        public ICollection<Appointment> Appointments { get; set; } //ilişki kurmak için
  
        public string Role { get; set; } = "doctor";
    }
}
