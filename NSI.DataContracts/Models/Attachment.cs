using Newtonsoft.Json;
using NSI.Common.DataContracts.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NSI.DataContracts.Models
{
    [Table("Attachment", Schema = "echo")]
    public class Attachment : BaseModelDto
    {
        [JsonProperty(PropertyName = "requestId")]
        public Guid RequestId { get; set; }

        [JsonProperty(PropertyName = "typeId")]
        public Guid TypeId { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [ForeignKey("RequestId")]
        public Request Request { get; set; }

        [ForeignKey("TypeId")]
        public DocumentType Type { get; set; }
        
        public Attachment(Guid requestId, Guid typeId)
        {
            RequestId = requestId;
            TypeId = typeId;
        }

        public Attachment(Guid requestId, Guid typeId, string url)
        {
            RequestId = requestId;
            TypeId = typeId;
            Url = url;
        }
    }
}
