using System.ComponentModel.DataAnnotations;

namespace Hospital.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Email zorunlu!")]
        [StringLength(30,ErrorMessage ="Email en fazla 30 karakter olabilir.")]
        public string Username { get; set; }

       // [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre zorunlu!")]
        [MinLength(6, ErrorMessage = "Şifre en fazla 6 karakter olabilir.")]
        [MaxLength(16, ErrorMessage = "Şifre en fazla 16 karakter olabilir.")]
        public string Password { get; set; }
    }




}
