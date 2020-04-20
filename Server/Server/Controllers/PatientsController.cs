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
    public class PatientsController : ModelControllerBase<Patient>
    {
        public PatientsController(IMongoDBSettings settings)
            : base(new MongoService<Patient>(settings, "Patients"))
        {

        }
    }
}