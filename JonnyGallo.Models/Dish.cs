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
    public class Dish : ObservableEntityData, IDish,ITaggable
    {
        private string _title;

        [JsonIgnore]
        public string DisplayName => ToString();

        [JsonProperty("_embedded.wp:featuredmedia[0].media_details.sizes.highgrove-medium.source_url")]
        public string ImgUrl { get; set; }

        [JsonProperty("excerpt.rendered")]
        public string Text { get; set; }

        [JsonProperty("title.rendered")]
        public string Title
        {
            get { return _title; }
            set
            {
                SetProperty(ref _title, value);
                // DisplayName is dependent on FirstName
                OnPropertyChanged("Title");
            }
        }
        [JsonProperty("_embedded.wp:term[0][*].name")]
        public string[] Tags { get; set; }

        public string Restaurant { get; set; }

        [JsonIgnore]
        public string Category { get; set; }

        //[JsonProperty("title.rendered")]
        //public string Title { get; set; }
        [JsonIgnore]
        public string Description { get; set; }
        [JsonIgnore]
        public string ImageUrl { get; set; }

        //[JsonProperty("_embedded.wp:term[0][0].name")]
        //public string Tag0 { get; set; }


        //public string Tag1 { get; set; }
    }

    public interface IDish
    {
        string Restaurant { get; set; }
        string Category { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        string ImageUrl { get; set; }
        
    }

    public interface ITaggable
    {
        string[] Tags { get; set; }
    }
}
