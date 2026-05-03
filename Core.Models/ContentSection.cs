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
    public class ContentSection : BaseUserNamable
    {
        public required ContentType ContentType { get; set; }
        public required int ContentTypeId { get; set; }
    }
}