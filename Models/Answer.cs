using System;
using System.Collections.Generic;

#nullable disable

namespace CertificationProject.Models {
    public partial class Answer {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public int QuestionId { get; set; }
        public string UserAnswer { get; set; }

        public virtual Question Question { get; set; }
        public virtual Result Session { get; set; }
    }
}