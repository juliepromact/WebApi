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
    public class UpdateController : ApiController
    {
        public UpdateController()
        {

        }
        private WebApiContext db = new WebApiContext();

        // GET api/Update123
        public IQueryable<Update> GetUpdate()
        {
            return db.Update;
        }

        //// GETONE api/UpdateOne/5
        //[ResponseType(typeof(Update))]
        //public IHttpActionResult GetUpdateOne(int id)
        //{
        //    Update update = db.Update.Find(id);
        //    if (update == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(update);
        //}



        // GET api/Update/5

        public List<Update> GetUpdate(int id)
        {
            return db.Update.Where(d => d.Product_ID == id).ToList();   
        }

        // PUT api/Update/5
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

        // POST api/Update/5
        [ResponseType(typeof(Update))]
        public IHttpActionResult PostUpdate(int id,Update update)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Update up = new Update()
            {
                UpdateIntro = update.UpdateIntro,
                UpdateDetail = update.UpdateDetail,
                Product_ID = id,
                UpdateDate = DateTime.Now
            };

            db.Update.Add(up);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = update.UpdateID }, update);
        }

        // DELETE api/Update/5
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