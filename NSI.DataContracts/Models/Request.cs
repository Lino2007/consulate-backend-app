using Newtonsoft.Json;
using NSI.Common.DataContracts.Base;
using NSI.Common.Enumerations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NSI.DataContracts.Models
{
    public class Request : BaseModelDto
    {
        [JsonProperty(PropertyName = "userId")]
        public Guid UserId { get; set; }

        [JsonProperty(PropertyName = "employeeId")]
        public Guid EmployeeId { get; set; }

        [JsonProperty(PropertyName = "dateCreated")]
        public DateTime DateCreated { get; set; }

        [JsonProperty(PropertyName = "reason")]
        public string Reason { get; set; }

        [JsonProperty(PropertyName = "type")]
        public RequestType Type { get; set; }

        [JsonProperty(PropertyName = "state")]
        public RequestState State { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("EmployeeId")]
        public User Employee { get; set; }
    }
}
