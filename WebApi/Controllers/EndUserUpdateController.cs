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
    public class EndUserUpdateController : ApiController
    {
        private WebApiContext db = new WebApiContext();

        // GET api/EndUserUpdate
        public IQueryable<Update> GetUpdate()
        {
            return db.Update;
        }

        // GET api/EndUserUpdate/5
     
        //original
        //[ResponseType(typeof(Update))]
        //public IHttpActionResult GetUpdate(int id)
        //{
        //    Update update = db.Update.Find(id);
        //    if (update == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(update);
        //}



        public IHttpActionResult GetUpdate(int id)
        {

            ProductNew pn = db.ProductNew
                .Include(i => i.Updates).Where(i=>i.ProductID==id)
                .Single();
            //var endUserProducts = new HashSet<int>(endUser.ProductNews.Select(c => c.ProductID));
            var abc = PopulateAssignedUpdateData(pn);
            return Ok(abc);
            //original
            //return db.EveryBody;
        }

        private List<Update> PopulateAssignedUpdateData(ProductNew pn)
        {
            var allUpdates = db.Update;
            var ownerupdates = new HashSet<int>(pn.Updates.Select(c => c.UpdateID));
            var viewModel = new List<Update>();
            foreach (var update in allUpdates)
            {
                if (ownerupdates.Contains(update.UpdateID))
                {
                    viewModel.Add(new Update
                    {
                        UpdateID = update.UpdateID,
                        UpdateIntro = update.UpdateIntro,
                        UpdateDetail=update.UpdateDetail
                       // Assigned = ownerupdates.Contains(product.ProductID)
                    });
                }
            }

            return viewModel;
        }
        // PUT api/EndUserUpdate/5
        public IHttpActionResult PutUpdate(int id, Update update)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != update.UpdateID)
            {
                return BadRequest();
            }

            db.Entry(update).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UpdateExists(id))
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

        // POST api/EndUserUpdate
        [ResponseType(typeof(Update))]
        public IHttpActionResult PostUpdate(Update update)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Update.Add(update);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = update.UpdateID }, update);
        }

        // DELETE api/EndUserUpdate/5
        [ResponseType(typeof(Update))]
        public IHttpActionResult DeleteUpdate(int id)
        {
            Update update = db.Update.Find(id);
            if (update == null)
            {
                return NotFound();
            }

            db.Update.Remove(update);
            db.SaveChanges();

            return Ok(update);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UpdateExists(int id)
        {
            return db.Update.Count(e => e.UpdateID == id) > 0;
        }
    }
}