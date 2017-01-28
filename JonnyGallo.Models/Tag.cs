using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace JonnyGallo.Models
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class Tag : ObservableEntityData, ITag
    {

        [JsonProperty("id")]
        public string WpId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public interface ITag
    {
        string Name { get; set; }
        string WpId { get; set; }
    }
}
