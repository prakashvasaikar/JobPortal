using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Sp_GetAllCandidateResponseModel
    {
        [Key]
        public int CandidateCode { get; set; }
        public string FullName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string VacancyType { get; set; }
        public string PrimarySkill { get; set; }
        public int ExperienceYear { get; set; }
        public int ExperienceMonth { get; set; }
        public string JobMode { get; set; }
        public string ReferBy { get; set; }
        public string Status { get; set; }
        public string ResumePath { get; set; }

    }
}
