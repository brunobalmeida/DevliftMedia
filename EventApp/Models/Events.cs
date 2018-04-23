using System;
using System.Collections.Generic;

namespace EventApp.Models
{
    public partial class Events
    {
        public int EventId { get; set; }
        public string EventTitle { get; set; }
        public DateTime? EventDate { get; set; }
        public byte[] Picture { get; set; }
        public string PictureType { get; set; }
        public string EventDescription { get; set; }
        public string EventContact { get; set; }
        public string EventLink { get; set; }
        public string EventAddress { get; set; }
    }
}
