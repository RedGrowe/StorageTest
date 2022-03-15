using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransferController : ControllerBase
    {
        private readonly ILogger<Transfer> _logger;
        private readonly IWebHostEnvironment _env;

        public TransferController(ILogger<Transfer> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return new JsonResult(db.Transfers.ToList());
            }
        }

        [HttpPost, Route("AddTransfer")]
        public void AddTransfer([FromBody] string value)
        {
            
            using(ApplicationDbContext db = new ApplicationDbContext())
            {
                Transfer tf = JsonConvert.DeserializeObject<Transfer>(value);
                Dictionary<string, int> dt = new Dictionary<string, int>();

                List<TransferPosition> DeserializePosition = JsonConvert.DeserializeObject<List<TransferPosition>>(tf.Position);
                
                foreach(var item in DeserializePosition)
                {
                    dt.Add(JsonConvert.SerializeObject(new Nomenclature { Id = item.Id, Name = item.Name }), item.Counter);
                }
                tf.Position = JsonConvert.SerializeObject(dt);
                db.Transfers.Add(tf);
                db.SaveChanges();
            }
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var TransferList = db.Transfers.ToList();
                db.Remove(db.Transfers.Where(x => x.Id == Guid.Parse(id)).FirstOrDefault());
                db.SaveChanges();
            }
        }
    }

    public class TransferPosition : Nomenclature
    {
        public int Counter { get; set; }
    }
}

