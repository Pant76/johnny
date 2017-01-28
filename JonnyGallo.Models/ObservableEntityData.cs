using System;
using JonnyGallo.Abstractions;
using MvvmHelpers;
using Newtonsoft.Json;

namespace JonnyGallo.Models
{
    public class ObservableEntityData : ObservableObject, IObservableEntityData
    {
        public ObservableEntityData()
        {
            Id = Guid.NewGuid().ToString().ToUpper();
        }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        //[CreatedAt]
        public DateTimeOffset CreatedAt { get; set; }

        //[UpdatedAt]
        public DateTimeOffset UpdatedAt { get; set; }

        //[Version]
        public byte[] Version { get; set; }
    }

}