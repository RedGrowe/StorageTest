using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NomenclatureController : ControllerBase
    {
        private readonly ILogger<Nomenclature> _logger;
        private readonly IWebHostEnvironment _env;

        public NomenclatureController(ILogger<Nomenclature> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return new JsonResult(db.Nomenclatures.ToList());
            }
        }

    }
}
