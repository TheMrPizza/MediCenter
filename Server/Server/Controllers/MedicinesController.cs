using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Server.Services.Abstract;
using Common;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicinesController : ControllerBase
    {
        private readonly IDBService _service;

        public MedicinesController(IDBService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<Medicine>> GetAll()
        {
            return _service.MedicinesService.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<Medicine> Get(string id)
        {
            Medicine medicine = _service.MedicinesService.Get(id);
            if (medicine == null)
            {
                return NotFound();
            }

            return medicine;
        }
    }
}