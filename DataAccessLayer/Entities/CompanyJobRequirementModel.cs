using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    [Table("CompanyJobRequirement")]
    public class CompanyJobRequirementModel
    {
        [Key]
        public int Id { get; set; }
        public int RefId_VacancyMaster { get; set; }
        public string Status { get; set; }
        public DateTime PostedOn { get; set; } = DateTime.Now;
        public DateTime? ExpiredOn { get; set; }
        public string JobMode { get; set; }
        public string JobDescription { get; set; }
        public bool IsActive { get; set; } = true;
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? UpdatedOn { get; set; } = DateTime.Now;
    }

}
