using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GroupService;
using LoginService.Models;
using PolicyService;

namespace PolicySetService.Models
{
    public class PolicySet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long PolicySetId { get; set; }

        [Required]
        public int PolicyId { get; set; }

        public long? LoginId { get; set; }

        public int? GroupId { get; set; }

        public bool? Selected { get; set; }

        public string PolicyParam { get; set; }

        public virtual Login UserLogin { get; set; }

        public virtual Group UserGroup { get; set; }

        public virtual Policy Policy { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}