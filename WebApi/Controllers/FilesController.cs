using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class FilesController : ApiController
    {
        // POST api/testupload
        [HttpPost] // This is from System.Web.Http, and not from System.Web.Mvc
        public async Task<HttpResponseMessage> Upload()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                this.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = GetMultipartProvider();
            var result = await Request.Content.ReadAsMultipartAsync(provider);

            // On upload, files are given a generic name like "BodyPart_26d6abe1-3ae1-416a-9429-b35f15e6e5d5"
            // so this is how you can get the original file name
            var originalFileName = GetDeserializedFileName(result.FileData.First());

            // uploadedFileInfo object will give you some additional stuff like file length,
            // creation time, directory name, a few filesystem methods etc..
            var uploadedFileInfo = new FileInfo(result.FileData.First().LocalFileName);
         
            // Through the request response you can return an object to the Angular controller
            // You will be able to access this in the .success callback through its data attribute
            // If you want to send something to the .error callback, use the HttpStatusCode.BadRequest instead
            var returnData = "ReturnTest";
            return this.Request.CreateResponse(HttpStatusCode.OK, new { returnData });
        }

        // You could extract these two private methods to a separate utility class since
        // they do not really belong to a controller class but that is up to you
        private MultipartFormDataStreamProvider GetMultipartProvider()
        {
            var uploadFolder = "~/App_Data/Tmp/FileUploads"; // you could put this to web.config
            var root = HttpContext.Current.Server.MapPath(uploadFolder);
            Directory.CreateDirectory(root);
            return new MultipartFormDataStreamProvider(root);
        }

        // Extracts Request FormatData as a strongly typed model
        private object GetFormData<T>(MultipartFormDataStreamProvider result)
        {
            if (result.FormData.HasKeys())
            {
                var unescapedFormData = Uri.UnescapeDataString(result.FormData
                    .GetValues(0).FirstOrDefault() ?? String.Empty);
                if (!String.IsNullOrEmpty(unescapedFormData))
                    return JsonConvert.DeserializeObject<T>(unescapedFormData);
            }

            return null;
        }

        private string GetDeserializedFileName(MultipartFileData fileData)
        {
            var fileName = GetFileName(fileData);
            return JsonConvert.DeserializeObject(fileName).ToString();
        }
        public string GetFileName(MultipartFileData fileData)
        {
            return fileData.Headers.ContentDisposition.FileName;
        }


        //public IHttpActionResult Upload(HttpFileCollection file,Media media,int id)
        //{
        //    try
        //    {
        //        if (file != null)
        //        {
        //            var count = file.Count;
        //            for (var i = 0; i < count; i++)
        //            {
        //                using (var binaryReader = new BinaryReader(file[i].InputStream))
        //                {
        //                    byte[] fileData = binaryReader.ReadBytes(file[i].ContentLength);
        //                    var content = file[i].ContentType;
        //                    return CreatedAtRoute("DefaultApi", new { id = media.MediaID },media);
        //                    //    HttpPostedFileBase file = Request.Files["OriginalLocation"];
        //                    //    newImage.MediaName = media.MediaName;
        //                    //    newImage.AlternateText = media.AlternateText;
        //                    //    newImage.VideoUrl = media.VideoUrl;
        //                    //    newImage.Discriminator = media.Discriminator;
        //                    //    //Here's where the ContentType column comes in handy.  By saving
        //                    //    //  this to the database, it makes it easier to get it back
        //                    //    //  later when trying to show the image.
        //                    //    newImage.ContentType = file.ContentType;

        //                    //    Int32 length = file.ContentLength;
        //                    //    //This may seem odd, but the fun part is that if
        //                    //    //  I didn't have a temp image to read into, I would
        //                    //    //  get memory issues for some reason.  Something to do
        //                    //    //  with reading straight into the object's ActualImage property.
        //                    //    byte[] tempImage = new byte[length];
        //                    //    file.InputStream.Read(tempImage, 0, length);
        //                    //    newImage.ImageData = tempImage;
        //                    //    newImage.Update_ID = id;

        //                    //    db.Media.Add(newImage);
        //                    //    db.SaveChanges();

        //                    //}




        //                    //var uploadDocumentpath = _azureCloudStorageRepository.UploadDocumentToCloud(
        //                    //    file[i].FileName, "documents", fileData);
        //                    //if (uploadDocumentpath != null)
        //                    //{
        //                    //    return uploadDocumentpath;
        //                    //}
        //                }
        //            }
        //        }
        //    }

        //    catch(Exception e)
        //    {
        //        return null;

        //    }
        //}

    }
}
