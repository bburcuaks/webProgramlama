using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Entities
{
    [Table("Admins")]
    public class Admin 
    {

        [Key]
        public Guid AdminId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        //admin girişi için

        [Required]
        [StringLength(30)]
        public string Username { get; set; }

        [Required]
        [StringLength(30)]
        public string Password { get; set; }
        public bool Locked { get; set; }

        public string Role { get; set; } = "admin"; // Yeni eklenen sütun

        


    }

}
