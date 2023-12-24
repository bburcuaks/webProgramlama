using System;
using System.ComponentModel.DataAnnotations;


namespace Hospital.Models
{
    public class MakeAppointmentViewModel
    {


        [Required(ErrorMessage = "Doktor seçimi zorunludur.")]
        [Display(Name = "Doktor")]
        public Guid DoctorId { get; set; }

        public Guid UserId { get; set; }

        [Display(Name = "Randevu ID")]
        public Guid RandevuId { get; set; }


        [Required(ErrorMessage = "Departman seçimi zorunludur.")]
        [Display(Name = "Department")]
        public String Department { get; set; }


        [Required(ErrorMessage = "Randevu günü zorunludur.")]
        [Display(Name = "Randevu Günü")]
        public DateTime AppointmentDate { get; set; }



        [Required(ErrorMessage = "Randevu saati zorunludur.")]
        [Display(Name = "Randevu Saati")]
        public DateTime AppointmentTime { get; set; }

       

        [Display(Name = "Zaman Damgası")]
        public DateTime Timestamp { get; set; }

    }
}