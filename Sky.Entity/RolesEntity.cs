using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sky.Entity
{
    [Table("roles")]
    //roles
    public class RolesEntity
    {

        /// <summary>
        /// ID
        /// </summary>
        public string ID
        {
            get;
            set;
        }
        /// <summary>
        /// RolesName
        /// </summary>
        public string RolesName
        {
            get;
            set;
        }
        /// <summary>
        /// CreateDate
        /// </summary>
        public string CreateDate
        {
            get;
            set;
        }
        /// <summary>
        /// Remark
        /// </summary>
        public string Remark
        {
            get;
            set;
        }
        /// <summary>
        /// UpdateDate
        /// </summary>
        public string UpdateDate
        {
            get;
            set;
        }

    }
}
