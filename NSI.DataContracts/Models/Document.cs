using Newtonsoft.Json;
using NSI.Common.DataContracts.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NSI.DataContracts.Models
{
    public class Document : BaseModelDto
    {
        [JsonProperty(PropertyName = "requestId")]
        public Guid RequestId { get; set; }

        [JsonProperty(PropertyName = "typeId")]
        public Guid TypeId { get; set; }

        [JsonProperty(PropertyName = "dateCreated")]
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        [JsonProperty(PropertyName = "dateOfExpiration")]
        public DateTime DateOfExpiration { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "active")]
        public bool Active { get; set; } = true;

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [ForeignKey("RequestId")]
        public Request Request { get; set; }

        [ForeignKey("TypeId")]
        public DocumentType Type { get; set; }

        public Document(Guid requestId, Guid typeId, DateTime dateOfExpiration, string url, string title)
        {
            RequestId = requestId;
            TypeId = typeId;
            DateOfExpiration = dateOfExpiration;
            Url = url;
            Title = title;
        }
    }
}
