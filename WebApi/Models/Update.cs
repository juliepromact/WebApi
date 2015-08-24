using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class Update
    {
        [Key]
        public int UpdateID { get; set; }
        [Display(Name = "Introduction")]
        public string UpdateIntro { get; set; }
        [Display(Name = "Detail")]
        public string UpdateDetail { get; set; }
        [Display(Name = "Date")]
        public DateTime UpdateDate { get; set; }
        public int Product_ID { get; set; }
        [ForeignKey("Product_ID")]
        public virtual ProductNew ProductNew { get; set; }
        //drastic change
        public virtual ICollection<Media> Media { get; set; }
    }

    public class CreateUpdate
    {
        public string UpdateIntro { get; set; }
        [Display(Name = "Detail")]
        public string UpdateDetail { get; set; }
        public DateTime UpdateDate { get; set; }
        public int Product_ID { get; set; }
    }
}