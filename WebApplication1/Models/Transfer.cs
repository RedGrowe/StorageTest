using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Transfer
    {
        public Guid Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }

        [Column(TypeName = "jsonb")]
        public string Position { get; set; }
    }
}
