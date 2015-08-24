using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class EndUser : EveryBody
    {
        public EndUser()
        {
            ProductNews = new List<ProductNew>();
        }
        //[EmailAddress]
        //public string Email { get; set; }
        public string Gender { get; set; }
        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }
        public virtual ICollection<ProductNew> ProductNews { get; set; }
    }
}