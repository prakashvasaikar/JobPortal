using DataAccessLayer.DbConnection;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Repository
{
    public interface IVacancyRepository
    {
        IEnumerable<VacancyMasterModel> getList();
        VacancyMasterModel getInfoById(int id);
        void saveVacancy(VacancyMasterModel model);
        void deleteById(int id);
    }
    public class VacancyRepository : IVacancyRepository
    {
        private readonly AetherDbContext _db;
        public VacancyRepository(AetherDbContext aetherDbContext)
        {
            _db = aetherDbContext;
        }
        public void deleteById(int id)
        {
            var data = _db.vacancyMaster.Where(x => x.Id == id).FirstOrDefault();
            if (data != null)
            {
                _db.vacancyMaster.Remove(data);
                _db.SaveChanges();
            }
        }

        public VacancyMasterModel getInfoById(int id)
        {
            return _db.vacancyMaster.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<VacancyMasterModel> getList()
        {
            return _db.vacancyMaster.ToList();
        }

        public void saveVacancy(VacancyMasterModel model)
        {
            if (model.Id == 0)
            {
                _db.Add(model);
            }
            else
            {
                var data = _db.vacancyMaster.Where(x => x.Id == model.Id).FirstOrDefault();
                data.VacancyType = model.VacancyType;
                data.Status = model.Status;
                data.PostedOn = model.PostedOn;
                data.ExpiredOn = model.ExpiredOn;
                data.IsActive = model.IsActive;
                data.CreatedBy = model.CreatedBy;
                data.CreatedOn = model.CreatedOn;
                data.UpdatedOn = model.UpdatedOn;
            }
            _db.SaveChanges();
        }
    }
}
