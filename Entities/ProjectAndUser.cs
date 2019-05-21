using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApi.Entities
{
    public class ProjectAndUser
    {
        public int ProjectId { get; set; }
        [JsonIgnore]
        public virtual Project Project { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
    }
}
