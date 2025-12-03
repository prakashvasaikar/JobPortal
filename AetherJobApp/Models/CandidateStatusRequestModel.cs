namespace AetherJobApp.Models
{
    public class CandidateStatusRequestModel
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public int ReviewBy { get; set; }
    }
}
