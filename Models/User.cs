using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

#nullable disable

namespace CertificationProject.Models {
    public partial class User {
        public User() {
            Results = new HashSet<Result>();
        }

        public enum RoleState {
            user,
            admin
        }

        public int Id { get; set; }

        public RoleState Role { get; set; }

        public string Name { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Result> Results { get; set; }
    }
}