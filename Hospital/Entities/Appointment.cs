using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Entities
{
    [Table("Appointments")]
    public class Appointment
    {
        [Key]
        public Guid RandevuId { get; set; }



        // İlişkiyi temsil eden özellik
        public Guid Id { get; set; }
        public User User { get; set; }
        public Guid DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public string Department { get; set; }


        public DateTime Timestamp { get; set; }

        public DateTime AppointmentTime { get; set; }
        public DateTime AppointmentDate { get; set; }


        


    }
}
