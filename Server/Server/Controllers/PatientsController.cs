using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Server.Services.Abstract;
using Common;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IDBService _service;

        public PatientsController(IDBService service)
        {
            _service = service;
        }

        [HttpGet("{username}/{password}")]
        public ActionResult<Patient> SignIn(string username, string password)
        {
            Patient patient = _service.PatientsService.SignIn(username, password);
            if (patient == null)
            {
                return NotFound();
            }

            return patient;
        }

        [HttpPost]
        public ActionResult<bool> Register(Patient patient)
        {
            return _service.PatientsService.Register(patient);
        }

        [HttpGet("{username}/{password}/visits")]
        public ActionResult<List<Visit>> GetVisits(string username, string password)
        {
            Patient patient = _service.PatientsService.SignIn(username, password);
            if (patient == null)
            {
                return Unauthorized();
            }

            return _service.PatientsService.GetVisits(username);
        }

        [HttpGet("{username}")]
        public ActionResult<string> GetName(string username)
        {
            Patient patient = _service.PatientsService.Get(username);
            if (patient == null)
            {
                return NotFound();
            }

            return patient.Name;
        }
    }
}