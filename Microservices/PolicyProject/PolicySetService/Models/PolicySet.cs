using System;
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
        public Guid PolicySetId { get; set; }

        [Required]
        public Guid PolicyId { get; set; }

        public Guid? LoginId { get; set; }

        public Guid? GroupId { get; set; }

        public bool? Selected { get; set; }

        public string PolicyParam { get; set; }

        public virtual Login UserLogin { get; set; }

        public virtual Group UserGroup { get; set; }

        public virtual Policy Policy { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}