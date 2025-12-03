using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    [Table("StateMaster")]
    public class StateMasterModel
    {
        [Key]
        public int Id { get; set; }
        public int RefId_CountryMaster { get; set; }
        public string StateName { get; set; }
    }
}
