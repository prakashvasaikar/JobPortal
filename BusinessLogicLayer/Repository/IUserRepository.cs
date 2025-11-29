using DataAccessLayer.DbConnection;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer.Repository
{
    public interface IUserRepository
    {
        void SaveRegistration(UserMasterModel model);
        UserMasterModel Login(string username, string password);
    }
    public class UserRepository : IUserRepository
    {
        private readonly AetherDbContext _db;
        public UserRepository(AetherDbContext aetherDbContext)
        {
            _db = aetherDbContext;
        }

        public UserMasterModel Login(string username, string password)
        {
            return _db.userMaster.FirstOrDefault(x => x.Username.ToLower() == username.ToLower() && x.Password == password && x.IsActive);
        }

        public void SaveRegistration(UserMasterModel model)
        {
            _db.Add(model);
            _db.SaveChanges();
        }
    }
}
