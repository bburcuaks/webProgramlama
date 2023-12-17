using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Entities
{
    [Table("Appointments")]
    public class Appointment:User
    {
        [ForeignKey("PatientId")]
        public Guid? PatientId { get; set; }
        public Patient Patient { get; set; }
        public string Department { get; set; }

        [ForeignKey("DoctorId")]
        public Guid? DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public DateTime Timestamp { get; set; }

        public DateTime AppointmentTime { get; set; }
        public DateTime AppointmentDate { get; set; }


        



    }
}
