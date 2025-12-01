using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    [Table("CandidateDetail")]
    public class CandidateDetailModel
    {
        [Key]
        public int Id { get; set; }
        public int RefId_CompanyRequirement { get; set; }
        public int RefId_UserMaster { get; set; }
        public string PrimarySkill { get; set; }
        public string Status { get; set; }
        public string Experience { get; set; }
        public string Resume { get; set; }
        public string ReferBy { get; set; }
        public int ReviewBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime ReviewDate { get; set; }
    }
}
