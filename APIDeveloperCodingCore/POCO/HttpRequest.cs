using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDeveloperCodingCore.POCO
{
    /// <summary>
    /// 
    /// </summary>
    public class HttpRequest
    {
        public string BaseAddress { get; set; } = string.Empty;

        public string EndPoint { get; set; } = string.Empty;

        public string? Token { get; set; }

        public object? SerializeData { get; set; }
    }
}
