using DMMS.Models.Base;
using DMMS.Models.BaseIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMMS.Models
{
    public class KeywordTopic : BaseUserNamable
    {
        public int KeywordId { get; set; }
        public required KeywordStat Keyword { get; set; }
    }
}