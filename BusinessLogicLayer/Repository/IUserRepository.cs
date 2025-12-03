using DataAccessLayer.DbConnection;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<Sp_GetAllUsersResponseModel>> getUserAll();
        void saveRegistration(UserMasterModel model);
        UserMasterModel login(string username, string password);
        void updateIsActiveStatus(int id,bool isactive);
        Task<IEnumerable<CountryMasterModel>> getCountryList();
        Task<IEnumerable<StateMasterModel>> getStatelistByCountryId(int id);
        Task<IEnumerable<CityMasterModel>> getCityListByCityId(int id);
    }
    public class UserRepository : IUserRepository
    {
        private readonly AetherDbContext _db;
        public UserRepository(AetherDbContext aetherDbContext)
        {
            _db = aetherDbContext;
        }
        public async Task<IEnumerable<Sp_GetAllUsersResponseModel>> getUserAll()
        {
            var data = await _db.sp_GetAllUsers
               .FromSqlRaw("EXEC Sp_GetAllUsers")
               .ToListAsync();
            return data;
        }

        public async Task<IEnumerable<CityMasterModel>> getCityListByCityId(int id)
        {
            return await _db.cityMaster.Where(x => x.RefId_StateMaster.Equals(id)).ToListAsync();
        }

        public async Task<IEnumerable<CountryMasterModel>> getCountryList()
        {
            return await _db.countryMaster.ToListAsync();
        }

        public async Task<IEnumerable<StateMasterModel>> getStatelistByCountryId(int id)
        {
            return await _db.stateMaster.Where(x=>x.RefId_CountryMaster.Equals(id)).ToListAsync();
        }

        public UserMasterModel login(string username, string password)
        {
            return _db.userMaster.FirstOrDefault(x => x.Username.ToLower() == username.ToLower() && x.Password == password);
        }

        public void saveRegistration(UserMasterModel model)
        {
            model.Role = "User";
            _db.Add(model);
            _db.SaveChanges();
        }
        public void updateIsActiveStatus(int id, bool isactive)
        {
            var data = _db.userMaster.FirstOrDefault(x => x.Id == id);
            if (data == null) return;

            data.IsActive = !data.IsActive;
            _db.SaveChanges();
        }
    }
}
