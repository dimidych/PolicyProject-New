using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupService
{
    public class Group
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid GroupId { get; set; }

        [Required]
        public string GroupName { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}