using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiHelper.Models
{
    public class DogPicturesModel
    {
        [JsonProperty("message")]
        public List<string> Pictures { get; set; }
    }
}
