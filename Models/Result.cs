using System;
using System.Collections.Generic;

#nullable disable

namespace CertificationProject.Models {
    public partial class Result {
        public int SessionId { get; set; }
        public int? UserId { get; set; }
        public int? Mark { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime? DateEnd { get; set; }

        public virtual User User { get; set; }
    }
}