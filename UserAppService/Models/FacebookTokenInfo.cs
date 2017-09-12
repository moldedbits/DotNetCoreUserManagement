using Newtonsoft.Json;

namespace UserAppService.Models
{
    public class FacebookTokenInfo
    {
        //[JsonProperty("data")]
        //public string Data
        //{
        //    get; set;
        //}

        [JsonProperty("app_id")]
        public string AppId { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("scopes")]
        public string [] Scopes { get; set; }

        [JsonProperty("application")]
        public string Application { get; set; }

        [JsonProperty("expires_at")]
        public string ExpireAt { get; set; }

        [JsonProperty("is_valid")]
        public bool IsValid { get; set; }

        [JsonProperty("issued_at")]
        public string IssuedAt { get; set; }
    }
}
