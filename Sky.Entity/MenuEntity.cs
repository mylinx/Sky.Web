using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


//Nhibernate Code Generation Template 1.0
//author:MythXin
//blog:www.cnblogs.com/MythXin
//Entity Code Generation Template
namespace Sky.Entity
{
	[Table("menu")]
	 	//menu
		public class MenuEntity
	{
	
      	/// <summary>
		/// id
        /// </summary>
        public  string id
        {
            get; 
            set; 
        }        
		/// <summary>
		/// parentid
        /// </summary>
        public  string parentid
        {
            get; 
            set; 
        }        
		/// <summary>
		/// menuname
        /// </summary>
        public  string menuname
        {
            get; 
            set; 
        }        
		/// <summary>
		/// path
        /// </summary>
        public  string path
        {
            get; 
            set; 
        }        
		/// <summary>
		/// meta_icon
        /// </summary>
        public  string meta_icon
        {
            get; 
            set; 
        }        
		/// <summary>
		/// createtime
        /// </summary>
        public  string createtime
        {
            get; 
            set; 
        }        
		/// <summary>
		/// remark
        /// </summary>
        public  string remark
        {
            get; 
            set; 
        }        
		   
	}
}