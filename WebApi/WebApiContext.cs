using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using WebApi.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using WebApi.Entities;

namespace WebApi
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        //  public bool ConfirmedEmail { get; set; }

        //to enter id of productowner into aspnetusers table
        public virtual ProductOwner ProductOwner { get; set; }
        public virtual EndUser EndUser { get; set; }
        //public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        //{
        //    // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
        //    var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
        //    // Add custom user claims here
        //    return userIdentity;
        //}
    }
   
  public class WebApiContext : IdentityDbContext<IdentityUser>
  //  public class WebApiContext : DbContext
    {
        public WebApiContext() 
            : base("DefaultConnection")
        { }
        public DbSet<EndUser> EndUser { get; set; }
        public DbSet<ProductOwner> ProductOwner { get; set; }
        public DbSet<EveryBody> EveryBody { get; set; }
        public DbSet<ProductNew> ProductNew { get; set; }
        public DbSet<Update> Update { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<UserModel> UserModel { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public System.Data.Entity.DbSet<WebApi.Models.OwnerRequest> OwnerRequests { get; set; }
    }
}
