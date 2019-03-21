using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


//Nhibernate Code Generation Template 1.0
//author:MythXin
//blog:www.cnblogs.com/MythXin
//Entity Code Generation Template
namespace Sky.Entity
{
	[Table("perssion")]
	 	//perssion
		public class PerssionEntity
	{
	
      	/// <summary>
		/// auto_increment
        /// </summary>
        public string ID
        {
            get; 
            set; 
        }        
		/// <summary>
		/// ParentID
        /// </summary>
        public string ParentID
        {
            get; 
            set; 
        }        
		/// <summary>
		/// Name
        /// </summary>
        public  string Name
        {
            get; 
            set; 
        }        
		/// <summary>
		/// LinkUrl
        /// </summary>
        public  string PathRouter
        {
            get; 
            set; 
        }

        /// <summary>
        /// Component
        /// </summary>
        public string Component
        {
            get;
            set;
        }

        /// <summary>
        /// Meta_title
        /// </summary>
        public string Meta_title
        {
            get;
            set;
        }

        /// <summary>
        /// Meta_icon
        /// </summary>
        public string Meta_icon
        {
            get;
            set;
        }

        /// <summary>
        /// Meta_content
        /// </summary>
        public string Meta_content
        {
            get;
            set;
        }

        /// <summary>
        /// LinkUrl
        /// </summary>
        public int? IsEnable
        {
            get; 
            set; 
        }       
		/// <summary>
		/// CreateDate
        /// </summary>
        public  string CreateDate
        {
            get; 
            set; 
        }        
		/// <summary>
		/// UpdateDate
        /// </summary>
        public  string UpdateDate
        {
            get; 
            set; 
        }        
		/// <summary>
		/// Remark
        /// </summary>
        public  string Remark
        {
            get; 
            set; 
        }        
		   
	}
}