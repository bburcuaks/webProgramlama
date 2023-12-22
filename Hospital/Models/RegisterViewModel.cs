﻿using System.ComponentModel.DataAnnotations;

namespace Hospital.Models
{
    public class RegisterViewModel
    {


        [Required(ErrorMessage = "Name is required.")]
        [MinLength(0, ErrorMessage = "İsim giriniz.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required.")]
        [MinLength(0, ErrorMessage = "Soyisim giriniz.")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Email zorunlu!")]
        [StringLength(30, ErrorMessage = "Email en fazla 30 karakter olabilir.")]
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
