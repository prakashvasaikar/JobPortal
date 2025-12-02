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
    public interface ICandidateRepository
    {
        IEnumerable<CandidateDetailModel> getList();
        CandidateDetailModel getInfoById(int id);
        void saveCandidate(CandidateDetailModel model);
        void deleteCandidateById(int id);
        CandidateDetailModel checkExistJobApply(int userid, int reuirementid);
        Task<List<Sp_GetAllCandidateResponseModel>> GetAllCandidates();
    }
    public class CandidateRepository : ICandidateRepository
    {
        private readonly AetherDbContext _db;
        public CandidateRepository(AetherDbContext aetherDbContext)
        {
            _db = aetherDbContext;
        }

        public CandidateDetailModel checkExistJobApply(int userid, int reuirementid)
        {
            return _db.candidateDetail.Where(x => x.RefId_UserMaster == userid && x.RefId_CompanyRequirement == reuirementid).FirstOrDefault();
        }

        public void deleteCandidateById(int id)
        {
            var data = _db.candidateDetail.Where(x => x.Id == id).FirstOrDefault();
            if (data != null)
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
        public async Task<List<Sp_GetAllCandidateResponseModel>> GetAllCandidates()
        {
            var candidates = await _db.sp_GetAllCandidate
                .FromSqlRaw("EXEC Sp_GetAllCandidate")
                .ToListAsync();
            return candidates;
        }
        public void saveCandidate(CandidateDetailModel model)
        {
            if (model.Id == 0)
            {
                _db.Add(model);
            }
            else
            {
                var data = _db.candidateDetail.Where(x => x.Id == model.Id).FirstOrDefault();

            }
            _db.SaveChanges();
        }
    }
}
