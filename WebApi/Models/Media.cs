using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class Media
    {
        
        public int MediaID { get; set; }
        public string VideoUrl { get; set; }
        public string MediaName { get; set; }
        public string AlternateText { get; set; }
        public string ContentType { get; set; }
        public byte[] ImageData { get; set; }
        public string Discriminator { get; set; }
        public int Update_ID { get; set; }
        [ForeignKey("Update_ID")]
        public virtual Update Update { get; set; }   
    }
}