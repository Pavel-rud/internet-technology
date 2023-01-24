using Domain.Models;
using System.Text.Json.Serialization;

namespace IT_Project.Serializers
{
    public class DoctorSerializer
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("specialization id")]
        public int SpecializationId { get; set; }
        [JsonPropertyName("fullname")]
        public string Fullname { get; set; } = string.Empty;
    }
}
