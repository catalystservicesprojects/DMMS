using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMMS.Models.ViewModels
{
    public class Affiliate
    {
        public int Id { get; set; }
        public required string Name { get; set; } = "";
        public string? Alias { get; set; } = "";
        //public required string BackgroundColor { get; set; }
        //public required string TextColor { get; set; }
        //public required string BorderColor { get; set; }
        //public required string Style { get; set; }
        //public required string Class { get; set; }
        //public required int OpenIn { get; set; }
        //public required int ContentId { get; set; }
        public int? SellerEntityId { get; set; }
        public decimal? Price { get; set; }
        public required string Link { get; set; }
    }
}
