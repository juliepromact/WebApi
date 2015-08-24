using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class ProductOwner : EveryBody
    {
        //[EmailAddress]
      //  public string Email { get; set; }
        [Display(Name = "Owner Name")]
        public string OwnerName { get; set; }
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [Display(Name = "Founded In")]
        public DateTime? FoundedIn { get; set; }
        public string Description { get; set; }
        public string WebsiteURL { get; set; }
        public string TwitterHandler { get; set; }
        public string FacebookPageURL { get; set; }
        public bool? Approval { get; set; }
        public bool? EmailConfirmed { get; set; }
        //drastic step
        public virtual ICollection<ProductNew> ProductNews { get; set; }
    }

    public class OwnerRequest
    {
        public int ID { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string OwnerName { get; set; }
        public bool? Approval { get; set; }
        public bool? EmailConfirmed { get; set; }
    }

}