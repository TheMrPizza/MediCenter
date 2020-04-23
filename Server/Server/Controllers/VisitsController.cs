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
    public class VisitsController : ControllerBase
    {
        private IDBService _service { get; }

        public VisitsController(IDBService service)
        {
            _service = service;
        }

        [HttpPost]
        public ActionResult<Visit> Schedule(Visit visit)
        {
            Doctor doctor = _service.DoctorsService.FindDoctorForVisit(visit);
            if (doctor == null)
            {
                return NotFound();
            }

            visit.Doctor = doctor;
            if (!_service.VisitsService.Schedule(visit))
            {
                return NotFound();
            }

            return visit;
        }
    }
}