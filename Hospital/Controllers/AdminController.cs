using Hospital.Entities;
using Hospital.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
namespace Hospital.Controllers
{
    [Authorize(Roles = "admin")]
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
                // Yeni doktor nesnesi oluştur
                var newDoctor = new Doctor
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Username = model.Username,
                    Password = model.Password,
                    Locked = false, // Varsayılan olarak kilidi açık yapabilirsiniz
                    DepartmentId = model.DepartmentId,
                    CalismaGunu = model.CalismaGunu,
                    BaslangicSaati = model.BaslangicSaati,
                    BitisSaati = model.BitisSaati,
                    Appointments = new List<Appointment>() // Başlangıçta boş bir randevu listesi
                };
                 

                //department veritabanından çekiyoruz
                newDoctor.Department = _databaseContext.Departments.FirstOrDefault(d => d.DeparmentId == model.DepartmentId);
                // Doktoru veritabanına ekleyin
                await _databaseContext.Doctors.AddAsync(newDoctor);
                await _databaseContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            // Eğer gerekli bilgiler eksikse formu tekrar göster
            return View(model);
        }




        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult ListDoctors()
        {
            var doctors = _databaseContext.Doctors.Include(d => d.Department).ToList();
            return View(doctors);
        }

        


    }

}