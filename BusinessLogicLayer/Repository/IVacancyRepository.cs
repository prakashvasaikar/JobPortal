using DataAccessLayer.DbConnection;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Repository
{
    public interface IVacancyRepository
    {
        Task<IEnumerable<VacancyMasterModel>> getList();
        VacancyMasterModel getInfoById(int id);
        void saveVacancy(VacancyMasterModel model);
        void deleteById(int id);
        void updateIsActiveStatus(int id, bool isactive);
        Task<IEnumerable<VacancyMasterModel>> getActiveList();
    }
    public class VacancyRepository : IVacancyRepository
    {
        private readonly AetherDbContext _db;
        public VacancyRepository(AetherDbContext aetherDbContext)
        {
            _db = aetherDbContext;
        }
        public VacancyMasterModel getInfoById(int id)
        {
            return _db.vacancyMaster.FirstOrDefault(x => x.Id == id);
        }
        public async Task<IEnumerable<VacancyMasterModel>> getList()
        {
            return await _db.vacancyMaster
                            .OrderByDescending(x => x.Id)
                            .ToListAsync(); // async EF call
        }
        public async Task<IEnumerable<VacancyMasterModel>> getActiveList()
        {
            return await _db.vacancyMaster.Where(x => x.IsActive).ToListAsync();
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
                data.IsActive = model.IsActive;
            }
            _db.SaveChanges();
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
        public void updateIsActiveStatus(int id, bool isActive)
        {
            var data = _db.vacancyMaster.FirstOrDefault(x => x.Id == id);
            if (data == null) return;
            data.IsActive = isActive;

            _db.SaveChanges();
        }
    }
}
