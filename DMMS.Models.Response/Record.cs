using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMMS.Models.Response
{
    public class Record
    {
        public int? id { get; set; }
        public int? content_id { get; set; }
        public int? status { get; set; }
        public string? mode { get; set; }
        public string? message { get; set; }
    }
}
