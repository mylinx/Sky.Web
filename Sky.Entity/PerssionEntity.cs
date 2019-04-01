using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sky.Entity
{
    [Table("perssion")]
    public class PerssionEntity
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
        /// RoutersID
        /// </summary>
        public string RoutersID
        {
            get;
            set;
        }
        /// <summary>
        /// Control
        /// </summary>
        public string Control
        {
            get;
            set;
        }
        /// <summary>
        /// Action
        /// </summary>
        public string Action
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
        /// UpdateDate
        /// </summary>
        public string UpdateDate
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

    }
}
