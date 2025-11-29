using DataAccessLayer.DbConnection;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Repository
{
    public interface ICandidateRepository
    {
        IEnumerable<CandidateDetailModel> getList();
        CandidateDetailModel getInfoById(int id);
        void saveCandidate(CandidateDetailModel model);
        void deleteCandidateById(int id);
    }
    public class CandidateRepository : ICandidateRepository
    {
        private readonly AetherDbContext _db;
        public CandidateRepository(AetherDbContext aetherDbContext)
        {
            _db = aetherDbContext;
        }
        public void deleteCandidateById(int id)
        {
            var data = _db.candidateDetail.Where(x => x.Id == id).FirstOrDefault();
            if(data!=null)
            {
                _db.candidateDetail.Remove(data);
                _db.SaveChanges();
            }
        }

        public CandidateDetailModel getInfoById(int id)
        {
            return _db.candidateDetail.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<CandidateDetailModel> getList()
        {
            return _db.candidateDetail.ToList();
        }

        public void saveCandidate(CandidateDetailModel model)
        {
            if(model.Id==0)
            {
                _db.Add(model);
            }
            else
            {
                var data = _db.candidateDetail.Where(x => x.Id == model.Id).FirstOrDefault();
                data.RefId_CompanyRequirement=model.RefId_CompanyRequirement;
                data.RefId_UserMaster = model.RefId_UserMaster;
                data.PrimarySkill = model.PrimarySkill;
                data.Status=model.Status;
                data.Resume = model.Resume;
                data.ReferBy = model.ReferBy;
                data.ReferBy=model.ReferBy;
                data.ReviewDate = model.ReviewDate;
            }
            _db.SaveChanges();
        }
    }   
}
