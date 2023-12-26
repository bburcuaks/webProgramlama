using System.ComponentModel.DataAnnotations;

namespace Hospital.Models
{
    public class RegisterViewModel
    {


        [Required(ErrorMessage = "İsim zorunlu.")]
        [MinLength(0, ErrorMessage = "İsim giriniz.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyad zorunlu.")]
        [MinLength(0, ErrorMessage = "Soyisim giriniz.")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Kullanıcı adı zorunlu!")]
        [StringLength(30, ErrorMessage = "Kullanıcı adı en fazla 30 karakter olabilir.")]
        public string Username { get; set; }
        

        // [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre zorunlu!")]
        [MinLength(6, ErrorMessage = "Şifre en fazla 6 karakter olabilir.")]
        [MaxLength(16, ErrorMessage = "Şifre en fazla 16 karakter olabilir.")]
        public string Password { get; set; }
        [Compare(nameof(Password))]
        public string RePassword { get; set; }
    }




}
