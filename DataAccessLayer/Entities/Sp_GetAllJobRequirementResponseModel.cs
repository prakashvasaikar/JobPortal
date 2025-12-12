using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Sp_GetAllJobRequirementResponseModel
    {
        [Key]
        public int Id { get; set; }
        public int RefId_VacancyMaster { get; set; }
        public string VacancyType { get; set; }
        public string Status { get; set; }
        public DateTime PostedOn { get; set; }
        public DateTime ExpiredOn { get; set; }
        public string JobMode { get; set; }
        public string JobDescription { get; set; }
        public bool IsActive { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
    }
}
