namespace AetherJobApp.Models
{
    public class CareerRequestModel
    {
        public int Id { get; set; }
        public int RefId_CompanyRequirement { get; set; }
        public int RefId_UserMaster { get; set; }
        public string PrimarySkill { get; set; }
        public string Status { get; set; }
        public int ExperienceYear { get; set; }
        public int ExperienceMonth { get; set; }
        //public string Resume { get; set; }
        public string ReferBy { get; set; }
        //public int ReviewBy { get; set; }
        //public DateTime ReviewDate { get; set; }
    }

}
