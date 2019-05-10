using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApi.Entities
{
    public class UserProject
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
