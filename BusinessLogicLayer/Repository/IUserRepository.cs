using DataAccessLayer.DbConnection;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer.Repository
{
    public interface IUserRepository
    {
        IEnumerable<UserMasterModel> getAll();
        void saveRegistration(UserMasterModel model);
        UserMasterModel login(string username, string password);
    }
    public class UserRepository : IUserRepository
    {
        private readonly AetherDbContext _db;
        public UserRepository(AetherDbContext aetherDbContext)
        {
            _db = aetherDbContext;
        }
        public IEnumerable<UserMasterModel> getAll()
        {
            return _db.userMaster.ToList();
        }
        public UserMasterModel login(string username, string password)
        {
            return _db.userMaster.FirstOrDefault(x => x.Username.ToLower() == username.ToLower() && x.Password == password && x.IsActive);
        }

        public void saveRegistration(UserMasterModel model)
        {
            _db.Add(model);
            _db.SaveChanges();
        }
    }
}
