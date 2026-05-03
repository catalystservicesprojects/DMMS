using DMMS.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMMS.Models
{
    public class Company : BaseNamable
    {
        public int Parent { get; set; }
        public int Order { get; set; }
    }
}