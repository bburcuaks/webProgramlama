﻿using Hospital.Entities;
using Hospital.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Hospital.Controllers
{
    [Authorize(Roles = "user")]
    public class AppointmentController : Controller
    {
      
        private readonly DatabaseContext _databaseContext;
        private readonly IConfiguration _configuration;
        public AppointmentController(DatabaseContext databaseContext, IConfiguration configuration)
        {
            _databaseContext = databaseContext;
            _configuration = configuration;
        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> MakeAppointment()
        {

            ViewBag.DoctorList = new SelectList(_databaseContext.Doctors, "DoctorId", "Name");
            ViewBag.DepartmentList = new SelectList(_databaseContext.Departments, "DeparmentId", "Name"); ;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult MakeAppointment(MakeAppointmentViewModel model)
        {

            if(ModelState.IsValid)
            {
                // Kullanıcının daha önce aynı tarih ve saatte randevu alıp almadığını kontrol et
                if (IsAppointmentTimeTaken(model.DoctorId, model.AppointmentDate, model.AppointmentTime,model.Department,model.RandevuId))
                {
                    ModelState.AddModelError("", "Seçtiğiniz tarih ve saatte başka bir randevu bulunmaktadır. Lütfen başka bir tarih veya saat seçin.");
                    ViewBag.DoctorList = new SelectList(_databaseContext.Doctors, "DoctorId", "Name");
                    return View(model);
                }
                Guid userId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
                model.UserId = userId;



                // Randevu alma işlemleri...

                var user = _databaseContext.Users.FirstOrDefault(u => u.Id == model.UserId);
                var doctor = _databaseContext.Doctors.FirstOrDefault(d => d.DoctorId == model.DoctorId);

                var appointment = new Appointment
                {
                    RandevuId = Guid.NewGuid(),
                    Id = model.UserId,
                    DoctorId = model.DoctorId,
                    AppointmentDate = model.AppointmentDate,
                    AppointmentTime = model.AppointmentTime,
                    Department=model.Department
                };

                _databaseContext.Appointments.Add(appointment);
                _databaseContext.SaveChanges();

                return RedirectToAction("ListAppointments", "Doctor");
            }




            ViewBag.DoctorList = new SelectList(_databaseContext.Doctors, "DoctorId", "Name");
            ViewBag.DepartmentList = new SelectList(_databaseContext.Departments, "DeparmentId", "Name");

            return View(model);

            
        }


        private bool IsAppointmentTimeTaken(Guid doctorId, DateTime randevuGunu, DateTime randevuSaati,string Departman,Guid randevuId)
        {
            // Seçilen tarih ve saatte başka bir randevu var mı kontrol et
            return _databaseContext.Appointments.Any(appointment =>
                appointment.DoctorId == doctorId &&
                appointment.Department==Departman&&
                appointment.RandevuId != randevuId&&
                appointment.AppointmentDate.Date == randevuGunu.Date && // Tarih kıyaslaması sadece gün, ay, yıl olarak yapılır
                appointment.AppointmentTime == randevuSaati);
               
            
        }



        [AllowAnonymous]
        [HttpPost]
        public IActionResult GetDoctorsByDepartment(Guid departmentId)
        {
            var doctors = _databaseContext.Doctors
                            .Where(d => d.DepartmentId == departmentId)
                            .Select(d => new
                            {
                                DoctorId = d.DoctorId,
                                Name = d.Name
                            })
                            .ToList();

            return Json(doctors);
        }




       
        [HttpPost]
        [Authorize(Roles = "user")]
        public IActionResult DeleteAppointments(Guid appointmentId)
        {
            // Kullanıcının Id'sini al
            Guid userId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));

            // Kullanıcının sadece belirli randevularını bul
            var userAppointments = _databaseContext.Appointments
                .Where(a => a.Id == userId && a.RandevuId == appointmentId)
                .ToList();

            // Kullanıcının randevularını veritabanından sil
            _databaseContext.Appointments.RemoveRange(userAppointments);
            _databaseContext.SaveChanges();

            // Silme işlemi tamamlandıktan sonra bir sayfaya yönlendir
            return RedirectToAction("ListAppointments","Admin");
        }














    

        

       


       
    }




}

