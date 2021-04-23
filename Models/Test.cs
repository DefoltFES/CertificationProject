using System;
using System.Collections.Generic;

#nullable disable

namespace CertificationProject.Models {
    public partial class Test {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public string Topic { get; set; }
    }
}