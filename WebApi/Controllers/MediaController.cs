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
using System.Web;
using System.IO;

namespace WebApi.Controllers
{
    public class MediaController : ApiController
    {
        private WebApiContext db = new WebApiContext();

        // GET api/Media123
        public IQueryable<Media> GetMedia()
        {
            return db.Media;
        }

        // GET api/Media/5

        //public List<Media> GetMedia(int id)
        //{
        //    return db.Media.Where(d => d.Update_ID == id).ToList();
        //}

        public IHttpActionResult GetMedia(int id)
        {

            Update up = db.Update
                .Include(i => i.Media).Where(i => i.UpdateID == id)
                .Single();
            //var endUserProducts = new HashSet<int>(endUser.ProductNews.Select(c => c.ProductID));
            var abc = PopulateAssignedMediaData(up);
            return Ok(abc);
            //original
            //return db.EveryBody;
        }

        private List<Media> PopulateAssignedMediaData(Update up)
        {
            var allMedia = db.Media;
            var ownermedia = new HashSet<int>(up.Media.Select(c => c.MediaID));
            var viewModel = new List<Media>();
            foreach (var media in allMedia)
            {
                if (ownermedia.Contains(media.MediaID))
                {
                    viewModel.Add(new Media
                    {
                        MediaID = media.MediaID,
                        MediaName = media.MediaName
                    });
                }
            }

            return viewModel;
        }

        //// GET api/Media/5
        //[ResponseType(typeof(Media))]
        //public IHttpActionResult GetMedia(int id)
        //{
        //original method
        //    Media media = db.Media.Find(id);
        //    if (media == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(media);
        //}

        // PUT api/Media/5
        public IHttpActionResult PutMedia(int id, Media media)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != media.MediaID)
            {
                return BadRequest();
            }

            db.Entry(media).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MediaExists(id))
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


      

        // POST api/Media/5
        [ResponseType(typeof(Media))]
        public IHttpActionResult PostMedia(int id,Media media)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Media newImage = new Media();
            int times = db.Media.Where(d => d.Update_ID == id).Count();
            // this is working

            if (times < 5)
            {
                //    HttpPostedFileBase file = Request.Files["OriginalLocation"];
                //    newImage.MediaName = media.MediaName;
                //    newImage.AlternateText = media.AlternateText;
                //    newImage.VideoUrl = media.VideoUrl;
                //    newImage.Discriminator = media.Discriminator;
                //    //Here's where the ContentType column comes in handy.  By saving
                //    //  this to the database, it makes it easier to get it back
                //    //  later when trying to show the image.
                //    newImage.ContentType = file.ContentType;

                //    Int32 length = file.ContentLength;
                //    //This may seem odd, but the fun part is that if
                //    //  I didn't have a temp image to read into, I would
                //    //  get memory issues for some reason.  Something to do
                //    //  with reading straight into the object's ActualImage property.
                //    byte[] tempImage = new byte[length];
                //    file.InputStream.Read(tempImage, 0, length);
                //    newImage.ImageData = tempImage;
                //    newImage.Update_ID = id;

                //    db.Media.Add(newImage);
                //    db.SaveChanges();

                //}


                Media up = new Media()
                {
                    MediaName = media.MediaName,
                    Update_ID = id
                    // MediaUpdateDate = DateTime.Now
                };

                db.Media.Add(up);
                db.SaveChanges();
            }     

            return CreatedAtRoute("DefaultApi", new { id = media.MediaID }, media);
        }


        //// POST api/Media2123/5
        //[ResponseType(typeof(Media))]
        //public IHttpActionResult PostMedia(Media media)
        //{
        //    //original
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Media.Add(media);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = media.MediaID }, media);
        //}

        // DELETE api/Media/5
        [ResponseType(typeof(Media))]
        public IHttpActionResult DeleteMedia(int id)
        {
            Media media = db.Media.Find(id);
            if (media == null)
            {
                return NotFound();
            }

            db.Media.Remove(media);
            db.SaveChanges();

            return Ok(media);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MediaExists(int id)
        {
            return db.Media.Count(e => e.MediaID == id) > 0;
        }
    }
}