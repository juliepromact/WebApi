using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Models;
using WebApi;

namespace WebApi.Controllers
{
    public class EveryBodyController : ApiController
    {
        private WebApiContext db = new WebApiContext();

        // GET api/EveryBody
    //original
        //public IQueryable<EveryBody> GetEveryBody()
        //{
        //    return db.EveryBody;
        //}


        public IHttpActionResult GetEveryBody()
        {
            var abc = PopulateRequestList();
            return Ok(abc);
            //return db.ProductOwner.Where(d => d.Approval == false);
            //return db.EveryBody;
        }

        private List<ProductOwner> PopulateRequestList()
        {
            var all = db.ProductOwner.Where(d => d.Approval == false);
            var viewModel = new List<ProductOwner>();
            foreach (var request in all)
            {
                viewModel.Add(new ProductOwner
                {
                    OwnerName = request.OwnerName,
                    CompanyName = request.CompanyName,
                    Email = request.Email,
                    ID = request.ID
                });
            }


            return viewModel;
        }



        // GET api/EveryBody/5
        //  //original
        //[ResponseType(typeof(ProductOwner))]
        //public IHttpActionResult GetEveryBody(int id)
        //{
        //    ProductOwner everybody = db.ProductOwner.Find(id);
        //    if (everybody == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(everybody);
        //}
        ////original


        [ResponseType(typeof(ProductOwner))]
        public IHttpActionResult GetEveryBody(int id)
        {
            ProductOwner po = db.ProductOwner.Find(id);
            if (po == null)
            {
                return NotFound();
            }

            System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage(
            new System.Net.Mail.MailAddress("julie@promactinfo.com", "Web Registration"),
            new System.Net.Mail.MailAddress(po.Email));
            m.Subject = "Email confirmation";
            m.To.Add("julie@promactinfo.com");


            //m.Body = string.Format("Dear {0}<BR/>Thank you for your registration, please click on the below link to complete your registration: <a href=\"{1}\"title=\"User Email Confirm\">{1}</a>",
            //po.OwnerName, Url.Link("api/Account/Register",
            //new { Token = po.ID, Email = po.Email }));

            string link = "http://localhost:1853/Index.html#/signup";
            m.Body = string.Format("Dear {0}<BR/>Thank you for your registration, please click on the below link to complete your registration :  <a href=\"" + link + "?id=" + po.ID + "&Email=" + po.Email + "\" >click here</a>", po.OwnerName);

            m.IsBodyHtml = true;

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("mail.promactinfo.com");
            smtp.Credentials = new System.Net.NetworkCredential("julie@promactinfo.com", "Promact2015");
            //  smtp.EnableSsl = true;

            smtp.Send(m);
            po.Approval = true;
            po.EmailConfirmed = false;
            db.Entry(po).State = EntityState.Modified;
            // db.ORequest.Add(ownerrequest);
            db.SaveChanges();

            return Ok(po);
        }


        // PUT api/EveryBody/5
        public IHttpActionResult PutEveryBody(int id, EveryBody everybody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != everybody.ID)
            {
                return BadRequest();
            }

            db.Entry(everybody).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EveryBodyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/EveryBody
        //[ResponseType(typeof(ProductOwner))]
        //public IHttpActionResult PostEveryBody(ProductOwner everybody)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.EveryBody.Add(everybody);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = everybody.ID }, everybody);
        //}


        [ResponseType(typeof(OwnerRequest))]
        public IHttpActionResult PostEveryBody(OwnerRequest or)
        {
            if (ModelState.IsValid)
            {
                var req = db.ProductOwner.Where(d => d.Email == or.Email).ToList().Count;
                if (req == 0)
                {

                    ProductOwner po = new ProductOwner()
                    {
                        Email = or.Email,
                        CompanyName = or.CompanyName,
                        OwnerName = or.OwnerName,
                        Approval = false
                    };
                    db.ProductOwner.Add(po);
                    db.SaveChanges();
                    return CreatedAtRoute("DefaultApi", new { id = or.ID }, or);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }

            // return CreatedAtRoute("DefaultApi", new { id = or.ID }, or);
            return null;
        }


        // DELETE api/EveryBody/5
        [ResponseType(typeof(EveryBody))]
        public IHttpActionResult DeleteEveryBody(int id)
        {
            EveryBody everybody = db.EveryBody.Find(id);
            if (everybody == null)
            {
                return NotFound();
            }

            db.EveryBody.Remove(everybody);
            db.SaveChanges();

            return Ok(everybody);
        }

        // APPROVE api/EveryBody/5
        [ResponseType(typeof(EveryBody))]
        public IHttpActionResult ApproveEveryBody(int id)
        {
            if (id == 0)
            {
                return BadRequest();
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductOwner ownerrequest = db.ProductOwner.Find(id);

            if (ownerrequest == null)
            {
                return BadRequest(ModelState);
                // return HttpNotFound();
            }

            System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage(
            new System.Net.Mail.MailAddress("julie@promactinfo.com", "Web Registration"),
            new System.Net.Mail.MailAddress(ownerrequest.Email));
            m.Subject = "Email confirmation";
            m.To.Add("julie@promactinfo.com");

            //m.Body = string.Format("Dear {0}<BR/>Thank you for your registration, please click on the below link to complete your registration: <a href=\"{1}\"title=\"User Email Confirm\">{1}</a>",
            //ownerrequest.OwnerName, Url.Action("ConfirmEmail", "Account",
            //new { Token = ownerrequest.ID, Email = ownerrequest.Email }, Request.Url.Scheme));

            m.Body = string.Format("Dear {0}<BR/>Thank you for your registration, please click on the below link to complete your registration: <a href=\"{1}\"title=\"User Email Confirm\">{1}</a>",
            ownerrequest.OwnerName);

            m.IsBodyHtml = true;

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("mail.promactinfo.com");
            smtp.Credentials = new System.Net.NetworkCredential("julie@promactinfo.com", "Promact2015");
            smtp.Send(m);
            ownerrequest.Approval = true;
            ownerrequest.EmailConfirmed = false;
            db.Entry(ownerrequest).State = EntityState.Modified;
            db.SaveChanges();
            return Ok(id);

            //db.EveryBody.Remove(everybody);
            //db.SaveChanges();
            // return CreatedAtRoute("DefaultApi", new { id = or.ID }, or);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private bool EveryBodyExists(int id)
        {
            return db.EveryBody.Count(e => e.ID == id) > 0;
        }
    }
}