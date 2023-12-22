using Hospital.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            return View();
        }

        //[Authorize(Roles = "admin")]
        //[HttpPost]
        /*public async Task< IActionResult> AddDoctor(string name, string surname, Guid departmentId)
        {

            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(surname))
            {
                // Yeni doktor nesnesi oluştur
                var newDoctor = new Doctor
                {
                    Name = name,
                    Surname = surname,
                    Department = _databaseContext.Departments.FirstOrDefault(d => d.DeparmentId == departmentId),
                    Appointments = new List<Appointment>() // Doktora ait randevu listesi, başlangıçta boş olabilir
                };

                // Doktoru veritabanına ekleyin
              await  _databaseContext.Doctors.AddAsync(newDoctor);
              await    _databaseContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            // Eğer gerekli bilgiler eksikse formu tekrar göster
            return View();
        } */
    }
}