using Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Fullname { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public Role Role { get; set; }
        public string UserName { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;        
    }
}
