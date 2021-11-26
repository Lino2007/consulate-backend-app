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
        public Guid? EmployeeId { get; set; }

        [JsonProperty(PropertyName = "dateCreated")]
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        [JsonProperty(PropertyName = "reason")]
        public string Reason { get; set; }

        [JsonProperty(PropertyName = "type")]
        [Column(TypeName = "nvarchar(50)")]
        public RequestType Type { get; set; }

        [JsonProperty(PropertyName = "state")]
        [Column(TypeName = "nvarchar(50)")]
        public RequestState State { get; set; } = RequestState.Pending;

        [ForeignKey("UserId")] public User User { get; set; }

        [ForeignKey("EmployeeId")] public User Employee { get; set; }

        public Request(Guid userId, string reason, RequestType type) : this(userId, null, reason, type) { }

        public Request(Guid userId, Guid? employeeId, string reason, RequestType type)
        {
            UserId = userId;
            EmployeeId = employeeId;
            Reason = reason;
            Type = type;
        }
    }
}