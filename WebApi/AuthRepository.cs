using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApi.Entities;
using WebApi.Models;

namespace WebApi
{
        public class AuthRepository : IDisposable
        {
            private WebApiContext _ctx;

            private UserManager<IdentityUser> _userManager;

            public AuthRepository()
            {
                _ctx = new WebApiContext();
                _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));
            }

          //  public async Task<IdentityResult> RegisterUser(UserModel userModel)
            public async Task<IdentityResult> RegisterUser(UserModel userModel)
            {
                if (userModel.DateOfBirth != null)
                {
                    IdentityUser user = new IdentityUser
                    {
                        UserName = userModel.UserName,
                    };
                    EndUser eu = new EndUser()
                    {
                        Street1 = userModel.Street1,
                        Street2 = userModel.Street2,
                        City = userModel.City,
                        State = userModel.State,
                        Country = userModel.Country,
                        PIN = userModel.PIN,
                        ContactNumber = userModel.ContactNumber,
                        //  DateOfBirth = userModel.DateOfBirth,
                        Email = userModel.UserName,
                        DateOfJoining = DateTime.Now

                    };
                    _ctx.EndUser.Add(eu);
                    _ctx.SaveChanges();

                    //EveryBody = new EveryBody
                    //{
                    //   //ID= 1,
                    //    Email=model.Email
                    //}



                    var result = await _userManager.CreateAsync(user, userModel.Password);
                }

                else
                {
                    var e = userModel.UserName;
                    ProductOwner po = _ctx.ProductOwner.FirstOrDefault(d => d.Email == e);
                    po.Email = userModel.UserName;
                    po.Description = userModel.Description;
                    po.DateOfJoining = DateTime.Now;
                    po.FoundedIn = userModel.FoundedIn;
                    po.Street1 = userModel.Street1;
                    po.Street2 = userModel.Street2;
                    po.City = userModel.City;
                    po.State = userModel.State;
                    po.Country = userModel.Country;
                    po.PIN = userModel.PIN;
                    po.ContactNumber = userModel.ContactNumber;
                    po.TwitterHandler = userModel.TwitterHandler;
                    po.WebsiteURL = userModel.WebsiteURL;
                    po.FacebookPageURL = userModel.FacebookPageURL;
                    _ctx.Entry(po).State = EntityState.Modified;
                    var result1 = _ctx.SaveChanges();

                    IdentityUser user = new IdentityUser
                    {
                        UserName = userModel.UserName
                    };

                    var result = await _userManager.CreateAsync(user, userModel.Password);

                    if (result.Succeeded && result1 > 0)
                    {
                        if (po.EmailConfirmed == false && po.Approval == true)
                        {
                            po.EmailConfirmed = true;
                            _ctx.Entry(po).State = EntityState.Modified;
                            var op1 = _ctx.SaveChanges();
                            user.EmailConfirmed = true;
                            //  var op = UserManager.UpdateAsync(user);
                            _userManager.Update(user);
                        }
                        //else
                        //{
                        //   go to error page
                        //}
                    }

                    return result;
                }
                return null;
            }

            public async Task<IdentityUser> FindUser(string userName, string password)
            {
                IdentityUser user = await _userManager.FindAsync(userName, password);
                return user;
            }

            public Client FindClient(string clientId)
            {
                var client = _ctx.Clients.Find(clientId);
                return client;
            }

            public async Task<bool> AddRefreshToken(RefreshToken token)
            {

                var existingToken = _ctx.RefreshTokens.Where(r => r.Subject == token.Subject && r.ClientId == token.ClientId).SingleOrDefault();
                if (existingToken != null)
                {
                    var result = await RemoveRefreshToken(existingToken);
                }

                _ctx.RefreshTokens.Add(token);
                return await _ctx.SaveChangesAsync() > 0;
            }

            public async Task<bool> RemoveRefreshToken(string refreshTokenId)
            {
                var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);

                if (refreshToken != null)
                {
                    _ctx.RefreshTokens.Remove(refreshToken);
                    return await _ctx.SaveChangesAsync() > 0;
                }

                return false;
            }

            public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
            {
                _ctx.RefreshTokens.Remove(refreshToken);
                return await _ctx.SaveChangesAsync() > 0;
            }

            public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
            {
                var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);

                return refreshToken;
            }

            public List<RefreshToken> GetAllRefreshTokens()
            {
                return _ctx.RefreshTokens.ToList();
            }

            public async Task<IdentityUser> FindAsync(UserLoginInfo loginInfo)
            {
                IdentityUser user = await _userManager.FindAsync(loginInfo);

                return user;
            }

            public async Task<IdentityResult> CreateAsync(IdentityUser user)
            {
                var result = await _userManager.CreateAsync(user);

                return result;
            }

            public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
            {
                var result = await _userManager.AddLoginAsync(userId, login);

                return result;
            }

            public void Dispose()
            {
                _ctx.Dispose();
                _userManager.Dispose();

            }
        }
    
}