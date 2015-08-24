using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models.ViewModels
{
    public class ProductnUpdate
    {
        // only one created for msdn many to many implementation
        public IEnumerable<EndUser> EndUser { get; set; }
        public IEnumerable<ProductNew> ProductNews { get; set; }
        public IEnumerable<Update> Updates { get; set; }
        public IEnumerable<Media> Media { get; set; }
    }
}