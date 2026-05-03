using DMMS.Models.Base;
using DMMS.Models.BaseIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMMS.Models
{
    public class KeywordStat : BaseUserNamable
    {
        public int Parent { get; set; }
        public int Order { get; set; }
        public int Primary { get; set; }
        public int Secondary { get; set; }
        public required string Keyword { get; set; }
        public required string Currency { get; set; }

        public int? AvgMonthlySearches { get; set; }

        public required string ThreeMonthChange { get; set; }
        public required string YoYChange { get; set; }

        public required string Competition { get; set; }

        public int? CompetitionIndexedValue { get; set; }

        public decimal? TopOfPageBidLow { get; set; }

        public decimal? TopOfPageBidHigh { get; set; }

        public decimal? AdImpressionShare { get; set; }

        public decimal? OrganicImpressionShare { get; set; }

        public decimal? OrganicAveragePosition { get; set; }

        public bool? InAccount { get; set; }

        public bool? InPlan { get; set; }

        public int? SearchesFeb2025 { get; set; }
        public int? SearchesMar2025 { get; set; }
        public int? SearchesApr2025 { get; set; }
        public int? SearchesMay2025 { get; set; }
        public int? SearchesJun2025 { get; set; }
        public int? SearchesJul2025 { get; set; }
        public int? SearchesAug2025 { get; set; }
        public int? SearchesSep2025 { get; set; }
        public int? SearchesOct2025 { get; set; }
        public int? SearchesNov2025 { get; set; }
        public int? SearchesDec2025 { get; set; }
        public int? SearchesJan2026 { get; set; }
    }
}