namespace AetherJobApp.Models
{
    public class UserMasterRequestModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public int RefId_CountryMaster { get; set; }
        public int RefId_StateMaster { get; set; }
        public int RefId_CityMaster { get; set; }

    }
}
