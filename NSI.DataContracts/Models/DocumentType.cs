using Newtonsoft.Json;
using NSI.Common.DataContracts.Base;

namespace NSI.DataContracts.Models
{
    public class DocumentType : BaseModelDto
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
