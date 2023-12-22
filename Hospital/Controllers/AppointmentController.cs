using Hospital.Entities;
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



        [HttpGet]
        public async Task<IActionResult> MakeAppointment()
        {
           
            // Gerekirse doktorlar, hastalar, departmanlar gibi seçenekleri ViewBag  üzerinden gönderebilirsiniz.
           ViewBag.Doctors =  await _databaseContext.Doctors.ToListAsync(); // Örnek: Veritabanından doktor listesini al
            ViewBag.Departments = await _databaseContext.Departments.ToListAsync(); // Örnek: Veritabanından departman listesini al

            return View();
        }




        [HttpPost]
        public IActionResult MakeAppointment(string userName, string doctorName, string department, DateTime appointmentDate, DateTime appointmentTime)
        {
            if (ModelState.IsValid)
            {
                // Hasta ve doktor bilgilerini al
                var user = _databaseContext.Users.FirstOrDefault(p => p.Username == userName);   //Username:user.cs
                var doctor = _databaseContext.Doctors.FirstOrDefault(d => d.Name == doctorName);

                if (user != null && doctor != null)
                {
                    // Yeni randevuyu oluştur
                    var newAppointment = new Appointment
                    {
                        Doctor = doctor, // Doktor adı yerine doktor nesnesini kullanıyoruz
                        Department = department,
                        AppointmentDate = appointmentDate,
                        AppointmentTime = appointmentTime,
                        Timestamp = DateTime.Now,
                        User = user

                    };

                    // Yeni randevuyu veritabanına ekleme
                    _databaseContext.Appointments.Add(newAppointment);
                    _databaseContext.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    // Kullanıcı veya doktor bulunamadı, uygun bir mesaj veya işlemi gerçekleştir
                    ModelState.AddModelError("", "Kullanıcı veya doktor bulunamadı.");
                }
            }
          
            // Model geçerli değilse, sayfayı tekrar göster
            return View();
        }






       

        






        //Randevu lisleteme
        public IActionResult Index()
        {
            var appointments = _databaseContext.Appointments.ToList();
            return View(appointments);
        }






        // Yeni randevu oluşturma sayfası
        public IActionResult Create()
        {
            // Gerekirse doktorlar, hastalar, kullanıcılar gibi seçenekleri ViewBag veya ViewData üzerinden gönderebilirsiniz.
            return View();
        }

        // Yeni randevu oluşturma işlemi
        [HttpPost]
        public IActionResult Create(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                // Yeni randevuyu veritabanına ekleme
                _databaseContext.Appointments.Add(appointment);
                _databaseContext.SaveChanges();
                return RedirectToAction("Index");
            }
            // Eğer model geçerli değilse, tekrar create sayfasını göster
            return View(appointment);
        }

        // Randevu detayları
        public IActionResult Details(Guid id)
        {



            var appointment = _databaseContext.Appointments.FirstOrDefault(a => a.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        // Randevu düzenleme sayfası
        public IActionResult Edit(Guid id)
        {
            var appointment = _databaseContext.Appointments.FirstOrDefault(a => a.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }


        // Randevu düzenleme işlemi
        [HttpPost]
        public IActionResult Edit(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                // Randevuyu güncelleme
                _databaseContext.Appointments.Update(appointment);
                _databaseContext.SaveChanges();
                return RedirectToAction("Index");
            }
            // Eğer model geçerli değilse, tekrar edit sayfasını göster
            return View(appointment);
        }


        // Randevu silme işlemi
        public IActionResult Delete(Guid id)
        {
            var appointment = _databaseContext.Appointments.FirstOrDefault(a => a.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var appointment = _databaseContext.Appointments.FirstOrDefault(a => a.Id == id);
            if (appointment != null)
            {
                // Randevuyu veritabanından silme
                _databaseContext.Appointments.Remove(appointment);
                _databaseContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }




}

