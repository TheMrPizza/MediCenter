using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Services;
using Common;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly DoctorsService _doctorsService;

        public DoctorsController(DoctorsService doctorsService)
        {
            _doctorsService = doctorsService;
        }

        [HttpGet]
        public ActionResult<List<Doctor>> Get()
        {
            return _doctorsService.GetAll();
        }

        [HttpGet("{id:length(24)}", Name = "GetDoctor")]
        public ActionResult<Doctor> Get(string id)
        {
            Doctor doctor = _doctorsService.Get(id);
            if (doctor == null)
            {
                return NotFound();
            }

            return doctor;
        }

        [HttpPost]
        public ActionResult<Doctor> Add(Doctor doctor)
        {
            _doctorsService.Add(doctor);
            return CreatedAtRoute("GetDoctor", new { id = doctor.Id.ToString() }, doctor);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Doctor doctor)
        {
            if (_doctorsService.Get(id) == null)
            {
                return NotFound();
            }

            _doctorsService.Update(id, doctor);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Remove(string id)
        {
            Doctor doctor = _doctorsService.Get(id);
            if (doctor == null)
            {
                return NotFound();
            }

            _doctorsService.Remove(doctor);
            return NoContent();
        }
    }
}