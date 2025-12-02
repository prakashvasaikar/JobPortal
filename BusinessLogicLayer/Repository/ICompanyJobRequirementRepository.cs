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
    public interface ICompanyJobRequirementRepository
    {
        Task<IEnumerable<object>> getList();
        CompanyJobRequirementModel getInfoById(int id);
        void saveJobRequirement(CompanyJobRequirementModel model);
        void deleteById(int id);
        void updateIsActiveStatus(int id, bool isactive);
        Task<IEnumerable<object>> getActiveList();
    }
    public class CompanyJobRequirementRepository : ICompanyJobRequirementRepository
    {
        private readonly AetherDbContext _db;
        public CompanyJobRequirementRepository(AetherDbContext aetherDbContext)
        {
            _db = aetherDbContext;
        }
        public void deleteById(int id)
        {
            var data = _db.companyJobRequirement.Where(x => x.Id == id).FirstOrDefault();
            if (data != null)
            {
                _db.companyJobRequirement.Remove(data);
                _db.SaveChanges();
            }
        }

        public async Task<IEnumerable<object>> getActiveList()
        {
            var data = from job in _db.companyJobRequirement
                       join vac in _db.vacancyMaster
                           on job.RefId_VacancyMaster equals vac.Id into gj
                       from subVac in gj.DefaultIfEmpty()
                       where job.IsActive
                       orderby job.Id descending
                       select new
                       {
                           job.Id,
                           job.RefId_VacancyMaster,
                           VacancyType = subVac != null ? subVac.VacancyType : null,
                           job.Status,
                           job.PostedOn,
                           job.ExpiredOn,
                           job.JobMode,
                           job.JobDescription,
                           job.IsActive,
                           job.CreatedBy,
                           job.CreatedOn,
                           job.UpdatedOn
                       };

            return await data.ToListAsync();
        }

        public CompanyJobRequirementModel getInfoById(int id)
        {
            return _db.companyJobRequirement.FirstOrDefault(x => x.Id == id);
        }

        public async Task<IEnumerable<object>> getList()
        {
            var data = from job in _db.companyJobRequirement
                       join vac in _db.vacancyMaster
                           on job.RefId_VacancyMaster equals vac.Id into gj
                       from subVac in gj.DefaultIfEmpty()   // LEFT JOIN
                       orderby job.Id descending
                       select new
                       {
                           job.Id,
                           job.RefId_VacancyMaster,
                           VacancyType = subVac != null ? subVac.VacancyType : null,
                           job.Status,
                           job.PostedOn,
                           job.ExpiredOn,
                           job.JobMode,
                           job.JobDescription,
                           job.IsActive,
                           job.CreatedBy,
                           job.CreatedOn,
                           job.UpdatedOn
                       };

            return await data.ToListAsync();
        }

        public void saveJobRequirement(CompanyJobRequirementModel model)
        {
            if (model.Id == 0)
            {
                _db.Add(model);
            }
            else
            {
                var data = _db.companyJobRequirement.Where(x => x.Id == model.Id).FirstOrDefault();
                data.RefId_VacancyMaster = model.RefId_VacancyMaster;
                data.PostedOn = model.PostedOn;
                data.ExpiredOn = model.ExpiredOn;
                data.JobMode = model.JobMode;
                data.JobDescription = model.JobDescription;
                data.UpdatedOn = DateTime.Now;
                data.Status = model.IsActive ? "Open" : "Closed";
                data.IsActive = model.IsActive;
            }
            _db.SaveChanges();
        }
        public void updateIsActiveStatus(int id, bool isActive)
        {
            var data = _db.companyJobRequirement.FirstOrDefault(x => x.Id == id);
            if (data == null) return;

            if (isActive)
            {
                data.Status = "Open";
                data.IsActive = true;
                data.PostedOn = DateTime.Now;
                data.ExpiredOn = DateTime.Now.AddMonths(1);
                data.UpdatedOn = DateTime.Now;
            }
            else
            {
                data.Status = "Closed";
                data.IsActive = false;
                data.UpdatedOn = DateTime.Now;
            }

            _db.SaveChanges();
        }
    }
}
