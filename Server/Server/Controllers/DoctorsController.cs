using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Server.Services.Abstract;
using Common;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDBService _service;

        public DoctorsController(IDBService service)
        {
            _service = service;
        }

        [HttpGet("{username}/{password}")]
        public ActionResult<Doctor> SignIn(string username, string password)
        {
            Doctor doctor = _service.DoctorsService.SignIn(username, password);
            if (doctor == null)
            {
                return NotFound();
            }

            return doctor;
        }

        [HttpPost]
        public ActionResult<bool> Register(Doctor doctor)
        {
            return _service.DoctorsService.Register(doctor);
        }

        [HttpGet("{username}/{password}/visits")]
        public ActionResult<List<Visit>> GetVisits(string username, string password)
        {
            Doctor doctor = _service.DoctorsService.SignIn(username, password);
            if (doctor == null)
            {
                return Unauthorized();
            }

            return _service.DoctorsService.GetVisits(username);
        }

        [HttpGet("{username}")]
        public ActionResult<string> GetName(string username)
        {
            Doctor doctor = _service.DoctorsService.Get(username);
            if (doctor == null)
            {
                return NotFound();
            }

            return doctor.Name;
        }
    }
}