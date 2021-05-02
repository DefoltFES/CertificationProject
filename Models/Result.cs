using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CertificationProject.Models
{
    public class Result
    {
        public int id { get; set; }
        public string question { get; set; }
        public string answer { get; set; }
        public string userAnswer { get; set; }
        public string userId { get; set; }

    }
}
