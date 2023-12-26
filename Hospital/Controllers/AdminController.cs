using Hospital.Entities;
using Hospital.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NETCore.Encrypt.Extensions;
using System.Collections.Generic;
using System.Security.Claims;

namespace Hospital.Controllers
{
  
    public class AdminController : Controller
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IConfiguration _configuration;
        public AdminController(DatabaseContext databaseContext, IConfiguration configuration)
        {
            _databaseContext = databaseContext;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }



        private string DoMD5HashedString(string s)
        {
            string md5Salt = _configuration.GetValue<string>("AppSettings:MD5Salt");
            string salted = s + md5Salt;
            string hashed = salted.MD5();
            return hashed;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult AddDoctor()
        {
            ViewBag.Departments = _databaseContext.Departments.ToList();
            return View();
        }




       
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> AddDoctor(AddDoctorViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_databaseContext.Doctors.Any(x => x.Username.ToLower() == model.Username.ToLower()))
                {
                    ModelState.AddModelError(nameof(model.Username), "Username is already exists.");
                    return View("Error");
                }
                else
                {
                    Doctor doctor = new()
                    {
                        Name = model.Name,
                        Surname = model.Surname,
                        Username = model.Username,
                        Password = DoMD5HashedString(model.Password),
                        DepartmentId = model.DeparmentId,
                        CalismaGunu = model.CalismaGunu,
                        BaslangicSaati = model.BaslangicSaati,
                        BitisSaati = model.BitisSaati,
                        Role = "doctor"
                    };

                    //department veritabanından çekiyoruz
                    doctor.Department = _databaseContext.Departments.FirstOrDefault(d => d.DeparmentId == model.DeparmentId);

                    _databaseContext.Doctors.Add(doctor);
                    int affectedRowCount = _databaseContext.SaveChanges();

                    if (affectedRowCount == 0)
                    {
                        ModelState.AddModelError("", "Kullanıcı Eklenemedi.");
                        return View("Error");
                    }
                    else
                    {
                        return RedirectToAction("ListDoctors", "Admin");
                    }
                }
            }
            return View(model);
        }





        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult ListDoctors()
        {
            var doctors = _databaseContext.Doctors.Include(d => d.Department).ToList();
            return View(doctors);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult ListUsers()
        {
            var users = _databaseContext.Users.ToList();
            return View(users);
        }
        [Authorize(Roles = "user")]
        [HttpGet]
        public IActionResult ListAppointments()
        {
            var appointments = _databaseContext.Appointments.ToList();
            return View(appointments);
        }



        [Authorize(Roles = "doctor")]
        [HttpGet]
        public IActionResult EditAppointment(Guid appointmentId)
        {
            // Kullanıcının Id'sini al
            Guid doctorId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));

            // Doktorun kendi randevularını bul
            var doctorAppointments = _databaseContext.Appointments
                .Where(a => a.DoctorId == doctorId && a.RandevuId == appointmentId)
                .FirstOrDefault();

            if (doctorAppointments == null)
            {
                // Bu randevu doktora ait değilse veya bulunamazsa hata sayfasına yönlendir
                return View("Error");
            }

            // Randevu düzenleme sayfasını göster
            return View(doctorAppointments);
        }

        [Authorize(Roles = "doctor")]
        [HttpPost]
        public IActionResult EditAppointment(Appointment editedAppointment)
        {
            if (ModelState.IsValid)
            {
                // Kullanıcının Id'sini al
                Guid doctorId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));

                // Doktorun kendi randevularını bul
                var doctorAppointments = _databaseContext.Appointments
                    .Where(a => a.DoctorId == doctorId && a.RandevuId == editedAppointment.RandevuId)
                    .FirstOrDefault();

                if (doctorAppointments == null)
                {
                    // Bu randevu doktora ait değilse veya bulunamazsa hata sayfasına yönlendir
                    return View("Error");
                }

                // Randevuyu güncelle
                doctorAppointments.AppointmentDate = editedAppointment.AppointmentDate;
                doctorAppointments.AppointmentTime = editedAppointment.AppointmentTime;

                // Veritabanını güncelle
                _databaseContext.SaveChanges();

                // Randevu düzenleme işlemi tamamlandıktan sonra bir sayfaya yönlendir
                return RedirectToAction("ListAppointments", "Admin");
            }

            // Eğer gerekli bilgiler eksikse formu tekrar göster
            return View(editedAppointment);
        }














        /*
  [Authorize(Roles = "user")]       
         [HttpGet]
         public IActionResult DeleteAppointments()            //bütün randevuları sildi.
         {
             // Kullanıcının Id'sini al
             Guid userId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
             // Kullanıcının randevularını bul
             var userAppointments = _databaseContext.Appointments.Where(a => a.Id == userId).ToList();

             // Kullanıcının randevularını veritabanından sil
             _databaseContext.Appointments.RemoveRange(userAppointments);
             _databaseContext.SaveChanges();

             // Silme işlemi tamamlandıktan sonra bir sayfaya yönlendir
             return RedirectToAction("Index");

         }
        */





    }

}