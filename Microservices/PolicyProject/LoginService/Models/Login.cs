using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GroupService;
using UserService.Models;

namespace LoginService.Models
{
    public class Login
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid LoginId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid GroupId { get; set; }

        [Required]
        public string LogIn { get; set; }

        public string Password { get; set; }
        
        public string Certificate { get; set; }

        public virtual User User { get; set; }

        public virtual Group Group { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}