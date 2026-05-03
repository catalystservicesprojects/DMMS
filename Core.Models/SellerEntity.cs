using DMMS.Models.Base;
using DMMS.Models.BaseIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMMS.Models
{
    public class SellerEntity : BaseNamable
    {
        public required string BackgroundColor { get; set; }
        public required string TextColor { get; set; }
        public required string BorderColor { get; set; }
        public required string Style { get; set; }
        public required string Class { get; set; }
        public required int OpenIn { get; set; }
    }
}
