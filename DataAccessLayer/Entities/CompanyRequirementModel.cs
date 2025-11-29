using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    [Table("CompanyRequirement")]
    public class CompanyRequirementModel
    {
        [Key]
        public int Id { get; set; }
        public int RefId_VacancyMaster { get; set; }
        public string JobMode { get; set; }
        public string Description { get; set; }
        public string Experience { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
