using System;
using System.Collections.Generic;

#nullable disable

namespace CertificationProject.Models {
    public partial class UsersAndTests {
        public int Id { get; set; }

        public int? UserId { get; set; }
        public int? TestId { get; set; }

        public virtual Test Test { get; set; }
        public virtual User User { get; set; }
    }
}