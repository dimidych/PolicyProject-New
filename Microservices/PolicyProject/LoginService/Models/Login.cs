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
        public long LoginId { get; set; }

        [Required]
        public long UserId { get; set; }

        [Required]
        public int GroupId { get; set; }

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