using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ReflectionAvalonia.Models.User
{
    public class User
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("username")]
        public string Username { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("address")]
        public Address Address { get; set; }
        [JsonPropertyName("phone")]
        public string Phone { get; set; }
        [JsonPropertyName("website")]
        public string Website { get; set; }
        [JsonPropertyName("company")]
        public Company Company { get; set; }

        public User(

            int id,string name,
            string username,string email,
            Address address,string phone,
            string website,Company company)
        {
            this.Id = id;
            this.Name = name;
            this.Username = username;
            this.Email = email;
            this.Address = address;
            this.Phone = phone;
            this.Company = company;
            this.Website = website;
        }    
    }
}
