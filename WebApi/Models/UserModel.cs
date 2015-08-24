using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class UserModel
    {
        [Key]
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string UserName { get; set; }
        [Display(Name="Date of Joining")]
        public DateTime? DateOfJoining { get; set; }

        public string DateOfBirth { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int PIN { get; set; }
        [Display(Name="Contact Number")]
        public int ContactNumber { get; set; }
        [Display(Name="Founded In")]
        public DateTime? FoundedIn { get; set; }
        public string Description { get; set; }
        public string WebsiteURL { get; set; }
        public string TwitterHandler { get; set; }
        public string FacebookPageURL { get; set; }
      
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}