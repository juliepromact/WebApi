using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class EveryBody
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int PIN { get; set; }
        public int ContactNumber { get; set; }

    }
}