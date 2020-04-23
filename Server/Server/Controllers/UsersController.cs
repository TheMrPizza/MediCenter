using Microsoft.AspNetCore.Mvc;
using Server.Services.Abstract;
using Common;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDBService _service;

        public UsersController(IDBService service)
        {
            _service = service;
        }

        [HttpGet("doctors/{username}/{password}")]
        public ActionResult<Doctor> SignInDoctor(string username, string password)
        {
            Doctor doctor = _service.DoctorsService.SignIn(username, password);
            if (doctor == null)
            {
                return NotFound();
            }

            return doctor;
        }

        [HttpGet("patients/{username}/{password}")]
        public ActionResult<Patient> SignInPatient(string username, string password)
        {
            Patient patient = _service.PatientsService.SignIn(username, password);
            if (patient == null)
            {
                return NotFound();
            }

            return patient;
        }

        [HttpPost("doctors")]
        public ActionResult<bool> Register(Doctor doctor)
        {
            return _service.DoctorsService.Register(doctor);
        }

        [HttpPost("patients")]
        public ActionResult<bool> Register(Patient patient)
        {
            return _service.PatientsService.Register(patient);
        }
    }
}