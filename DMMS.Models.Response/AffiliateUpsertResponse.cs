namespace DMMS.Models.Response
{
    public class AffiliateUpsertResponse
    {
        public int? Status { get; set; }
        public int? ContentId { get; set; }
        public List<Record>? Records { get; set; }
        public string? Message { get; set; }
    }
}
