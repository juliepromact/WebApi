using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Models;
using WebApi;
using WebApi.Models.ViewModels;

namespace WebApi.Controllers
{
    public class EndUserController : ApiController
    {
        private WebApiContext db = new WebApiContext();

        // GET api/EndUser
        public IHttpActionResult GetEveryBody()
        {
            var e = User.Identity.Name;

            if (e == null)
            {
                return null;
            }
            EndUser endUser= db.EndUser
                .Include(i => i.ProductNews)
                .Where(i => i.Email == e)
                .Single();
            //var endUserProducts = new HashSet<int>(endUser.ProductNews.Select(c => c.ProductID));
            var abc=PopulateAssignedProductData(endUser);
            return Ok(abc);
            //original
            //return db.EveryBody;
        }

        private List<AssignedProductData> PopulateAssignedProductData(EndUser endUser)
        {
            var allProducts = db.ProductNew;
            var endUserProducts = new HashSet<int>(endUser.ProductNews.Select(c => c.ProductID));
            var viewModel = new List<AssignedProductData>();
            foreach (var product in allProducts)
            {
                viewModel.Add(new AssignedProductData
                {
                    ProductID = product.ProductID,
                    ProductName = product.ProductName,
                    Assigned = endUserProducts.Contains(product.ProductID)
                });
            }

            return viewModel;
        }

        // POST api/EndUser
        [ResponseType(typeof(EndUser))]
           public IHttpActionResult PostEndUser(AssignedProductData[] selectedProducts)
        {

            var e = User.Identity.Name;
            if (e == null)
            {
                return null;
            }
            var endUserToUpdate = db.EndUser
               .Include(i => i.ProductNews)
              .Where(i => i.Email == e)
               .Single();

            try
            {

                UpdateEndUserProducts(selectedProducts, endUserToUpdate);
                db.SaveChanges();
               // return null;
                
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            PopulateAssignedProductData(endUserToUpdate);
            return Ok(endUserToUpdate);
        }

        ////original
        // if (!ModelState.IsValid)
        // {
        //     return BadRequest(ModelState);
        // }

        // db.EveryBody.Add(enduser);
        // db.SaveChanges();

        // return CreatedAtRoute("DefaultApi", new { id = enduser.ID }, enduser);

        private void UpdateEndUserProducts(AssignedProductData[] selectedProducts, EndUser endUserToUpdate)
        {
            if (selectedProducts == null)
            {
                endUserToUpdate.ProductNews = new List<ProductNew>();
                return;
            }

            var selectedProductsHS = new HashSet<int>(selectedProducts.Where(i=>i.Assigned).Select(i=>i.ProductID));
              //  Select(c=>c.ProductID));

            var endUserProducts = new HashSet<int>(endUserToUpdate.ProductNews.Select(c => c.ProductID));
            foreach (var product in db.ProductNew)
            {
                if (selectedProductsHS.Contains(product.ProductID))
                {
                    if (!endUserProducts.Contains(product.ProductID))
                    {
                        endUserToUpdate.ProductNews.Add(product);
                    }
                }
                else
                {
                    if (endUserProducts.Contains(product.ProductID))
                    {
                        endUserToUpdate.ProductNews.Remove(product);
                    }
                }
            }
        }


        //// GET api/EndUser/5
        //[ResponseType(typeof(EndUser))]
        //public IHttpActionResult GetEndUser(int id)
        //{
        //    EndUser enduser = db.EveryBody.Find(id);
        //    if (enduser == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(enduser);
        //}

        //// PUT api/EndUser/5
        //public IHttpActionResult PutEndUser(int id, EndUser enduser)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != enduser.ID)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(enduser).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!EndUserExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}


        //// DELETE api/EndUser/5
        //[ResponseType(typeof(EndUser))]
        //public IHttpActionResult DeleteEndUser(int id)
        //{
        //    EndUser enduser = db.EveryBody.Find(id);
        //    if (enduser == null)
        //    {
        //        return NotFound();
        //    }

        //    db.EveryBody.Remove(enduser);
        //    db.SaveChanges();

        //    return Ok(enduser);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool EndUserExists(int id)
        //{
        //    return db.EveryBody.Count(e => e.ID == id) > 0;
        //}
    }
}