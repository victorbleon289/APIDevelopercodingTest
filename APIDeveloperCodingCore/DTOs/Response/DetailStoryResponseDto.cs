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
    public class DetailStoryResponseDto
    {
        [JsonPropertyName("title")]
        public string Title { get; set; } = null!;

        [JsonPropertyName("uri")]
        public string Uri { get; set; } = null!;

        [JsonPropertyName("postedBy")]
        public string PostedBy { get; set; } = null!;

        [JsonPropertyName("time")]
        public string Time { get; set; } = null!;

        [JsonPropertyName("score")]
        public int Score { get; set; }

        [JsonPropertyName("commentCount")]
        public int CommentCount { get; set; }
    }
}
