using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    [Table("CityMaster")]
    public class CityMasterModel
    {
        [Key]
        public int Id { get; set; }
        public int RefId_StateMaster { get; set; }
        public string CityName { get; set; }
    }
}
