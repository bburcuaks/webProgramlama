using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace Hospital.Entities
{
    [Table("Users")]
    public class User
    {

        [Key]

        public Guid Id { get; set; }
       
        [Required]
        [StringLength(30)]
        public string Username { get; set; }
        [Required]
        [MinLength(3)]
        public string Password { get; set; }
        public bool Locked { get; set; }

        public DateTime CreatedAt { get; set; }= DateTime.Now;

        [Required]
        [StringLength(50)]

        public string Role { get; set; } = "user";
        public ICollection<Appointment> Appointments { get; set; } // İlişkiyi temsil ediyor.
    }
}
