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
    public class productNewController : ApiController
    {
        private WebApiContext db = new WebApiContext();

        //original
        //        public IQueryable<ProductNew> GetProductNew()
        //{
        //    var e = User.Identity.Name;
        //    ProductOwner po = db.ProductOwner.FirstOrDefault(d => d.Email == e);
        //    return db.ProductNew;   
        //}


       
        //original
        //public List<ProductNew> GetProductNew()
        //{
        //    var e = User.Identity.Name;
        //    //EveryBody eb = db.EveryBody.FirstOrDefault(d => d.Email == e);

        //    //  return db.ProductNew.Where(d => d.ProductOwner_ID == eb.ID).ToList();

        //    ProductOwner po = db.ProductOwner.FirstOrDefault(d => d.Email == e);
        //    return db.ProductNew.Where(d => d.ProductOwner_ID == po.ID).ToList();

        //}

        //this too works 
        //public IHttpActionResult GetProductNew()
        //{
        //    var e = User.Identity.Name;
        //    try
        //    {
        //        ProductOwner po = db.ProductOwner.FirstOrDefault(d => d.Email == e);
        //        var abc = db.ProductNew.Where(d => d.ProductOwner_ID == po.ID).ToList();
        //        return Ok(abc);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex);
        //    }
        //}


        // GET api/productNew
        public IHttpActionResult GetProductNew()
        {
            var e = User.Identity.Name;

            if (e == null)
            {
                return null;
            }
            ProductOwner po = db.ProductOwner
                .Include(i => i.ProductNews)
                .Where(i => i.Email == e)
                .Single();
            //var endUserProducts = new HashSet<int>(endUser.ProductNews.Select(c => c.ProductID));
            var abc = PopulateAssignedProductData(po);
            return Ok(abc);
            //original
            //return db.EveryBody;
        }

        private List<AssignedProductData> PopulateAssignedProductData(ProductOwner productowner)
        {
            var allProducts = db.ProductNew;
            var endUserProducts = new HashSet<int>(productowner.ProductNews.Select(c => c.ProductID));
            var viewModel = new List<AssignedProductData>();
            foreach (var product in allProducts)
            {
                if (endUserProducts.Contains(product.ProductID))
                {
                    viewModel.Add(new AssignedProductData
                    {
                        ProductID = product.ProductID,
                        ProductName = product.ProductName,
                        Assigned = endUserProducts.Contains(product.ProductID)
                    });
                }
            }

            return viewModel;
        }



        // GET api/productNew/5
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

        // PUT api/productNew/5
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

            var e = User.Identity.Name;
            ProductOwner po = db.ProductOwner.FirstOrDefault(d => d.Email == e);
            productnew.ProductOwner_ID = po.ID;


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

        // POST api/productNew

        //[ResponseType(typeof(ProductNew))]
        //public IHttpActionResult PostProductNew(ProductNew productnew)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var e = User.Identity.Name;
        //    ProductOwner po = db.ProductOwner.FirstOrDefault(d => d.Email == e);
        //    productnew.ProductOwner_ID = po.ID;


        //    db.ProductNew.Add(productnew);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = productnew.ProductID }, productnew);
        //}

        [ResponseType(typeof(AssignedProductData))]
        public IHttpActionResult PostProductNew(AssignedProductData productnew)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var e = User.Identity.Name;
            ProductOwner po = db.ProductOwner.FirstOrDefault(d => d.Email == e);
            ProductNew pn = new ProductNew()
            {
                ProductName = productnew.ProductName,
                ProductOwner_ID = po.ID
            };

            db.ProductNew.Add(pn);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = productnew.ProductID }, productnew);
        }
     

        //original
        //[ResponseType(typeof(ProductNew))]
        //public IHttpActionResult PostProductNew(ProductNew productnew)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    db.ProductNew.Add(productnew);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = productnew.ProductID }, productnew);
        //}
        //original


        // DELETE api/productNew/5
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