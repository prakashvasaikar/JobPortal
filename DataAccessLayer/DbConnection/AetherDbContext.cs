using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DbConnection
{
    public class AetherDbContext:DbContext
    {
        public AetherDbContext(DbContextOptions<AetherDbContext> option) : base(option) { }
        public DbSet<RoleMasterModel> roleMaster { get; set; }
        public DbSet<VacancyMasterModel> vacancyMaster { get; set; }
        public DbSet<UserMasterModel> userMaster { get; set; }
        public DbSet<CompanyJobRequirementModel> companyJobRequirement { get; set; }
        public DbSet<CandidateDetailModel> candidateDetail { get; set; }
        public DbSet<CityMasterModel> cityMaster { get; set; }
        public DbSet<CountryMasterModel> countryMaster { get; set; }
        public DbSet<StateMasterModel> stateMaster { get; set; }
        public DbSet<Sp_GetAllCandidateResponseModel> sp_GetAllCandidate { get; set; }
        public DbSet<Sp_GetAllUsersResponseModel> sp_GetAllUsers { get; set; }
        public DbSet<Sp_GetAllJobRequirementResponseModel> sp_GetAllFindJob { get; set; }
    }
}
