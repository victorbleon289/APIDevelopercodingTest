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
    /// <typeparam name="T"></typeparam>
    public class GenericResponseDto<T>
    {
        [JsonPropertyName("data")]
        public T Data { get; set; }
    }
}
