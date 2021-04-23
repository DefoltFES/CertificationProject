using System;
using System.Collections.Generic;

#nullable disable

namespace CertificationProject.Models {
    public partial class Question {
        public int Id { get; set; }
        public string QuestionString { get; set; }
        public string Answer { get; set; }
        public string Topic { get; set; }
    }
}