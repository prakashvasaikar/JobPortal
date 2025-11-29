using DataAccessLayer.DbConnection;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Repository
{
    public interface ICompanyRequirementRepository
    {
        IEnumerable<CompanyRequirementModel> getList();
        CompanyRequirementModel getInfoById(int id);
        void saveCompanyRequirement(CompanyRequirementModel model);
        void deleteById(int id);
    }
    public class CompanyRequirementRepository : ICompanyRequirementRepository
    {
        private readonly AetherDbContext _db;
        public CompanyRequirementRepository(AetherDbContext aetherDbContext)
        {
            _db = aetherDbContext;
        }
        public void deleteById(int id)
        {
            var data = _db.companyRequirement.Where(x => x.Id == id).FirstOrDefault();
            if (data != null)
            {
                _db.companyRequirement.Remove(data);
                _db.SaveChanges();
            }
        }

        public CompanyRequirementModel getInfoById(int id)
        {
            return _db.companyRequirement.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<CompanyRequirementModel> getList()
        {
            return _db.companyRequirement.ToList();
        }

        public void saveCompanyRequirement(CompanyRequirementModel model)
        {
            if (model.Id == 0)
            {
                _db.Add(model);
            }
            else
            {
                var data = _db.companyRequirement.Where(x => x.Id == model.Id).FirstOrDefault();
                data.RefId_VacancyMaster = model.RefId_VacancyMaster;
                data.JobMode = model.JobMode;
                data.Description = model.Description;
                data.Experience = model.Experience;
                data.CreatedBy = model.CreatedBy;
                data.CreatedOn = model.CreatedOn;
            }
            _db.SaveChanges();
        }
    }
}
