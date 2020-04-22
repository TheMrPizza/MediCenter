using Microsoft.AspNetCore.Mvc;
using Server.Services;
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

        [HttpGet("{username}/{password}")]
        public ActionResult<IPerson> SignIn(string username, string password)
        {
            // Cannot convert from IPerson to ActionResult<IPerson>, so checking each one separately
            Doctor doctor = _service.SignInDoctor(username, password);
            if (doctor != null)
            {
                return doctor;
            }

            Patient patient = _service.SignInPatient(username, password);
            if (patient != null)
            {
                return patient;
            }

            return NotFound();
        }

        [HttpPost]
        [Route("doctors")]
        public ActionResult<bool> Register(Doctor doctor)
        {
            return _service.Register(doctor);
        }

        [HttpPost]
        [Route("patients")]
        public ActionResult<bool> Register(Patient patient)
        {
            return _service.Register(patient);
        }
    }
}