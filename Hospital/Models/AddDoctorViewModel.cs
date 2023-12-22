using System.ComponentModel.DataAnnotations;

namespace Hospital.Models
{
    public class AddDoctorViewModel
    {

        [Required(ErrorMessage = "Ad alanı zorunlu.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyad alanı zorunlu.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Kullanıcı adı zorunlu.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Şifre zorunlu.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Lütfen bir departman seçin.")]
        public Guid DepartmentId { get; set; }

        [Required(ErrorMessage = "Lütfen çalışma gününü seçin.")]
        public DayOfWeek CalismaGunu { get; set; }

        [Required(ErrorMessage = "Lütfen başlangıç saati seçin.")]
        public TimeSpan BaslangicSaati { get; set; }

        [Required(ErrorMessage = "Lütfen bitiş saati seçin.")]
        public TimeSpan BitisSaati { get; set; }
    }
}
