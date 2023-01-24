using Domain.Models;
using System.Text.Json.Serialization;

namespace IT_Project.Serializers
{
    public class UserSerializer
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("username")]
        public string username { get; set; } = string.Empty;
        [JsonPropertyName("fullname")]
        public string Fullname { get; set; } = string.Empty;
        [JsonPropertyName("phone number")]
        public string PhoneNumber { get; set; } = string.Empty;
        [JsonPropertyName("role")]
        public Role Role { get; set; }
    }
}
