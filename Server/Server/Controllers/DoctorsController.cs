using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Services;
using Server.Models;
using Common;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ModelControllerBase<Doctor>
    {
        public DoctorsController(IMongoDBSettings settings) : base(new MongoService<Doctor>(settings))
        {
            
        }
    }
}