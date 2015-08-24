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
using WebApi.Models.ViewModels;

namespace WebApi.Controllers
{
    public class EndUserProductController : ApiController
    {
        private WebApiContext db = new WebApiContext();

        // GET api/EndUserProduct
        //public IHttpActionResult GetProductNew()
        //{
        //    var e = User.Identity.Name;
        //       // EndUser endUser = db.EndUser
        //       //   .Include(i => i.ProductNews)
        //       //     .Where(i => i.Email == e)
        //       //       .Single();
        //       // return endUser.ProductNews.ToList();

        //        var viewModel = new ProductnUpdate();
        //        viewModel.EndUser = db.EndUser
        //            .Include(i => i.ProductNews);          
        //        viewModel.ProductNews = viewModel.EndUser.Where(i => i.Email == e).Single().ProductNews;
        //        var abc= viewModel;
        //        return Ok(abc);
        //}

        public IHttpActionResult GetProductNew()
        {
            var e = User.Identity.Name;

            if (e == null)
            {
                return null;
            }
            EndUser endUser = db.EndUser
                .Include(i => i.ProductNews)
                .Where(i => i.Email == e)
                .Single();
            //var endUserProducts = new HashSet<int>(endUser.ProductNews.Select(c => c.ProductID));
            var abc = PopulateAssignedProductData(endUser);
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

     

        // GET api/EndUserProduct/5
        [ResponseType(typeof(ProductNew))]
        public IHttpActionResult GetProductNew(int id)
        {
            ProductNew productnew = db.ProductNew.Find(id);
            if (productnew == null)
            {
                return NotFound();
            }

            return Ok(productnew);
        }

        // PUT api/EndUserProduct/5
        public IHttpActionResult PutProductNew(int id, ProductNew productnew)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productnew.ProductID)
            {
                return BadRequest();
            }

            db.Entry(productnew).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductNewExists(id))
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

        // POST api/EndUserProduct
        [ResponseType(typeof(ProductNew))]
        public IHttpActionResult PostProductNew(ProductNew productnew)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductNew.Add(productnew);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = productnew.ProductID }, productnew);
        }

        // DELETE api/EndUserProduct/5
        [ResponseType(typeof(ProductNew))]
        public IHttpActionResult DeleteProductNew(int id)
        {
            ProductNew productnew = db.ProductNew.Find(id);
            if (productnew == null)
            {
                return NotFound();
            }

            db.ProductNew.Remove(productnew);
            db.SaveChanges();

            return Ok(productnew);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductNewExists(int id)
        {
            return db.ProductNew.Count(e => e.ProductID == id) > 0;
        }
    }
}