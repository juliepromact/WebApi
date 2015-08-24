using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class ProductNew
    {
        [Key]
        public int ProductID { get; set; }
        [Display(Name = "Prodcut Name")]
        public string ProductName { get; set; }

        public int ProductOwner_ID { get; set; }
        [ForeignKey("ProductOwner_ID")]
        public virtual ProductOwner ProductOwner { get; set; }
        //ente cheithu
        public virtual ICollection<Update> Updates { get; set; }
        ////ente cheithu
        public virtual ICollection<EndUser> EndUser { get; set; }
    }
}