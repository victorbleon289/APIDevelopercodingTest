using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace APIDeveloperCodingCore.DTOs.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class DetailsResponseDto
    {

        [JsonPropertyName("by")]
        public string By { get; set; } = null!;

        [JsonPropertyName("descendants")]
        public int Descendants { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("kids")]
        public long[] Kids { get; set; }

        [JsonPropertyName("score")]
        public int Score { get; set; }

        [JsonPropertyName("time")]
        public long Time { get; set; } 

        [JsonPropertyName("title")]
        public string Title { get; set; } = null!;

        [JsonPropertyName("type")]
        public string Type { get; set; } = null!;

        [JsonPropertyName("url")]
        public string Url { get; set; } = null!;

    }
}
