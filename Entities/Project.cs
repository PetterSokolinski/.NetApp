using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApi.Entities
{
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectId { get; set; }
        [Required, MaxLength(200)]
        public string Title { get; set; }
        public string Company { get; set; }
        [DefaultValue(true)]
        public bool Running { get; set; }
        public int? UserID { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
    }
}
