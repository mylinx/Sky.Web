using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

 
namespace Sky.Entity
{
    [Table("pessiondetail")]
    //pessiondetail
    public class PessiondetailEntity
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
        /// PerssionID
        /// </summary>
        public string PerssionID
        {
            get;
            set;
        }
        /// <summary>
        /// BtName
        /// </summary>
        public string BtName
        {
            get;
            set;
        }
        /// <summary>
        /// IsEnable
        /// </summary>
        public bool IsEnable
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

    }
}
