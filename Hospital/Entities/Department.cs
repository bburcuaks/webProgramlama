using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Entities
{

    [Table("Departments")]
    public class Department
    {

       
            [Key] // Primary key olduğunu belirtmek için
            public int Id { get; set; }

            public string Name { get; set; }
        // Diğer özellikleri buraya ekleyebilirsiniz
        public List<Doctor> Doctors { get; set; }

    }
}