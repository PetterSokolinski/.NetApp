using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace BackendApi.Entities
{
    public class Task
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskId { get; set; }
        [Required, MaxLength(200)]
        public string Title { get; set; }
        public string ToStart { get; set; }
        public string ToFinish { get; set; }
        [DefaultValue(false)]
        public bool Finished { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public string Description { get; set; }
        public int? UserID { get; set; }
        public int? ProjectID { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
        [JsonIgnore]
        public virtual Project Project { get; set; }
    }
}
