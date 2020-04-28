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
        public List<Medicine> GetAll()
        {
            return _service.MedicinesService.GetAll();
        }
    }
}