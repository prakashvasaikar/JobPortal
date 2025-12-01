using DataAccessLayer.DbConnection;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer.Repository
{
    public interface IUserRepository
    {
        IEnumerable<UserMasterModel> getAll();
        void saveRegistration(UserMasterModel model);
        UserMasterModel login(string username, string password);
        void updateIsActiveStatus(int id,bool isactive);
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
