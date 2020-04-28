using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Server.Services.Abstract;
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
            if (!_service.PatientsService.CheckNewVisit(visit))
            {
                return BadRequest();
            }

            Doctor doctor = _service.DoctorsService.FindDoctorForVisit(visit);
            if (doctor == null)
            {
                return NotFound();
            }

            visit.DoctorUsername = doctor.Username;
            Patient patient = _service.PatientsService.Get(visit.PatientUsername);
            if (!_service.VisitsService.Schedule(visit) ||
                !_service.DoctorsService.ScheduleVisit(doctor, visit) ||
                !_service.PatientsService.ScheduleVisit(patient, visit))
            {
                return NotFound();
            }

            return visit;
        }

        [HttpPut("{id}")]
        public ActionResult<Visit> GivePrescriptions(Prescription prescription)
        {
            Visit visit = _service.VisitsService.Get(prescription.VisitId);
            if (visit == null)
            {
                return NotFound();
            }

            Patient patient = _service.PatientsService.Get(visit.PatientUsername);
            Visit updatedVisit = _service.VisitsService.AddPrescriptions(visit, patient, prescription.Medicines);
            if (updatedVisit == null)
            {
                return NotFound();
            }

            return updatedVisit;
        }
    }
}