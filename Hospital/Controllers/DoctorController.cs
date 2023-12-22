using Hospital.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Controllers
{
    public class DoctorController : Controller
    {
        private readonly DatabaseContext _databaseContext;

        public DoctorController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        [HttpGet]
        public IActionResult GetDoctorsByDepartment(Guid departmentId)
        {
            var doctors = _databaseContext.Doctors.Where(d => d.DepartmentId == departmentId).ToList();
            return Json(doctors);
        }
    }
}

