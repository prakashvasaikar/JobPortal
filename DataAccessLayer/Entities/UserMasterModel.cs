using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    [Table("UserMaster")]
    public class UserMasterModel
    {
        [Key]
        public int Id { get; set; }
        public string Role { get; set; } ///as User Id
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } 
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? UpdatedOn { get; set; }
    }

}
