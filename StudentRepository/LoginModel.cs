using System.Text.Json.Serialization;

namespace StudentRepository
{
    public class LoginModel
    {
        [JsonPropertyName("userName")]
        public string? UserName { get; set; }

        [JsonPropertyName("password")]
        public string? Password { get; set; }
    }
}