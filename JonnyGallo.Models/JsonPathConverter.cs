using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace JonnyGallo.Models
{
    public class JsonPathConverter : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType,
                                        object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            object targetObj = Activator.CreateInstance(objectType);

            foreach (PropertyInfo prop in objectType.GetRuntimeProperties()
                                                    .Where(p => p.CanRead && p.CanWrite))
            {
                JsonPropertyAttribute att = prop.GetCustomAttributes(true)
                                                .OfType<JsonPropertyAttribute>()
                                                .FirstOrDefault();

                string jsonPath = (att != null ? att.PropertyName : prop.Name);


                if (prop.PropertyType.IsArray) //prop.PropertyType.GetGenericTypeDefinition()==typeof(List<string>)
                {


                    var arr = jo.SelectTokens(jsonPath).Select(i => i.ToString()).ToArray();



                    prop.SetValue(targetObj, arr, null);
                    return targetObj;

                }
                var token = jo.SelectToken(jsonPath);

                if (token != null && token.Type != JTokenType.Null)
                {
                    object value = token.ToObject(prop.PropertyType, serializer);
                    if (value.GetType() == typeof(string))
                    {
                        prop.SetValue(targetObj, CleanText((string)value), null);
                    }
                    else
                        prop.SetValue(targetObj, value, null);
                }
            }

            return targetObj;
        }

        private string CleanText(string htmlText)
        {
            return Regex.Replace(htmlText, "<.*?>", String.Empty);
        }

        public override bool CanConvert(Type objectType)
        {
            // CanConvert is not called when [JsonConverter] attribute is used
            return false;
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value,
                                       JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
