using Newtonsoft.Json;
using NSI.Common.DataContracts.Base;
using NSI.Common.Enumerations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NSI.DataContracts.Models
{
    public class User : BaseModelDto
    {
        [JsonProperty(PropertyName = "dateCreated")]
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "gender")]
        [Column(TypeName = "nvarchar(50)")]
        public Gender Gender { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "placeOfBirth")]
        public string PlaceOfBirth { get; set; }

        [JsonProperty(PropertyName = "dateOfBirth")]
        public DateTime DateOfBirth { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "active")]
        public bool Active { get; set; } = true;

        public User(string firstName, string lastName, Gender gender, string email, string username, string placeOfBirth, DateTime dateOfBirth, string country)
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            Email = email;
            Username = username;
            PlaceOfBirth = placeOfBirth;
            DateOfBirth = dateOfBirth;
            Country = country;
        }
    }
}
