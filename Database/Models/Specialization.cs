using Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models
{
    public class Specialization
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;       
    }
}
