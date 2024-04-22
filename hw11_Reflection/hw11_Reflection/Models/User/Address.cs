using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ReflectionAvalonia.Models.User
{
    public class Address
    {
        [JsonPropertyName("street")]
        public string Street { get; set; }
        [JsonPropertyName("suite")]
        public string Suite { get; set; }
        [JsonPropertyName("city")]
        public string City { get; set; }
        [JsonPropertyName("zipcode")]
        public string Zipcode { get; set; }
        [JsonPropertyName("geo")]
        public Geo Geo { get; set; }

        public Address(string street, string suite, string city, string zipcode, Geo geo)
        {
            this.Street = street;
            this.Suite = suite;
            this.City = city;
            this.Zipcode = zipcode;
            this.Geo = geo;
        }
    }
}
