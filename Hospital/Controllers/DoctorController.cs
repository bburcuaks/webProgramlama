using Hospital.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hospital.Controllers
{
    public class DoctorController : Controller
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IConfiguration _configuration;
        public DoctorController(DatabaseContext databaseContext, IConfiguration configuration)
        {
            _databaseContext = databaseContext;
            _configuration = configuration;
        }





        public IActionResult Index()
        {
            return View();
        }


        public IActionResult ListAppointments()
        {
            // Kullanıcının Id'sini al
            Guid doctorId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));

            // Doktorun randevularını çek
            var doctorAppointments = _databaseContext.Appointments
                .Where(a => a.DoctorId == doctorId)
                .ToList();

            // Gerekirse randevuları sırala veya başka işlemler yapabilirsiniz
            // Örneğin, sıralama için:
            // doctorAppointments = doctorAppointments.OrderBy(a => a.AppointmentDate).ToList();

            // View'e randevuları iletebilirsiniz
            return View(doctorAppointments);

        }
        [HttpPost]
        [Authorize(Roles = "doctor")]
        public IActionResult DeleteAppointments(Guid appointmentId)
        {
            // Kullanıcının Id'sini al
            Guid doctorId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));

            // Doktorun kendi randevusunu bul
            var doctorAppointment = _databaseContext.Appointments
                .FirstOrDefault(a => a.DoctorId == doctorId && a.RandevuId == appointmentId);

            if (doctorAppointment == null)
            {
                // Belirli randevu bulunamazsa hata sayfasına yönlendir
                return RedirectToAction("Error");
            }

            // Doktorun kendi randevusunu veritabanından sil
            _databaseContext.Appointments.Remove(doctorAppointment);
            _databaseContext.SaveChanges();

            // Silme işlemi tamamlandıktan sonra bir sayfaya yönlendir
            return RedirectToAction("ListAppointments", "Doctor");
        }




    }
}
