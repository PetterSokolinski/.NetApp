using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.ComponentModel;

namespace BackendApi.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [MaxLength(50)]
        public string Username { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Created { get; set; }
        [DefaultValue("User")]
        public string Role { get; set; }
        public string Token { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public ICollection<ProjectAndUser> Projects { get; set; }
    }
}
