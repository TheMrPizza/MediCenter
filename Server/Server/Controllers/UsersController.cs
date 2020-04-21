using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Services;
using Server.Config;
using Common;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _service;

        public UsersController(UsersService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("doctors")]
        public ActionResult<bool> Add(Doctor doctor)
        {
            return _service.RegisterDoctor(doctor);
        }

        [HttpPost]
        [Route("patients")]
        public bool Add(Patient patient)
        {
            return _service.RegisterPatient(patient);
        }
    }
}