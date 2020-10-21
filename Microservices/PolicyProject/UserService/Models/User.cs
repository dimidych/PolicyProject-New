using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long UserId { get; set; }

        [Required]
        public string UserLastName { get; set; }

        [Required]
        public string UserFirstName { get; set; }

        public string UserMiddleName { get; set; }

        public virtual string UserName => string.Concat(UserFirstName, " ", UserLastName, " ", UserMiddleName).Trim();

        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}