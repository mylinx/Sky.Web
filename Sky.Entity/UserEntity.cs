using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace Sky.Entity
{
    [Table("userentity")]
    public class UserEntity
    {
        [Key]
        public string ID { get; set; }
        public string RoleID { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Email { get; set; }
        public int? IsLock { get; set; }
        public string LoginLastDate { get; set; }
        public string CreateDate { get; set; }
        public string Remark { get; set; }

        [ForeignKey("RoleID")]
        public RolesEntity roles { get; set; }
         
    }
}
