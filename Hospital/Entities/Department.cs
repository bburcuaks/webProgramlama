using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Entities
{

    [Table("Departments")]
    public class Department
    {

        [Key]
        public Guid DeparmentId { get; set; }

        public string Name { get; set; }

        public ICollection<Doctor> Doctors { get; set; }

    }
}