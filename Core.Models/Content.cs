using DMMS.Models.Base;
using DMMS.Models.BaseIdentity;
using DMMS.WebMVC.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMMS.Models
{
    public class Content : BaseUserNamable
    {
        public Website? Website { get; set; }
        public int WebsiteId { get; set; }
        public required string ShortName { get; set; }
        public required string ShortDescription { get; set; }
        //public required int Version { get; set; }
        //public required int Website { get; set; }
        //public required int Publisher { get; set; }
    }
}