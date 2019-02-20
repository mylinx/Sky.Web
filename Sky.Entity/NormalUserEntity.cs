using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sky.Entity
{
    [Serializable()]
    [Table("NormalUser")]
    public class NormalUserEntity
    {

        public string  ID { get; set; }
        public string  Name { get; set; }
        public string  LockNumber { get; set; }
        public string  Account { get; set; }
        public string  Password { get; set; }
        public string  Mail { get; set; }
        public string  Tel { get; set; }
        public DateTime?  CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? LoginDate { get; set; }
        public string  Remark { get; set; }
        public int ?  IsReadExplain { get; set; }
        public int ? LoginState { get; set; }
        public DateTime? LOCKTIMEEND { get; set; }

    }
}
