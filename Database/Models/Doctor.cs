using System.ComponentModel.DataAnnotations.Schema;

using SpecializationDB = Database.Models.Specialization;

namespace Database.Models
{
    public class Doctor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SpecializationId;
        public string Fullname { get; set; } = string.Empty;
        
    }
}
