using Newtonsoft.Json;
using NSI.Common.DataContracts.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace NSI.DataContracts.Models
{
    [Table("DocumentType", Schema = "echo")]
    public class DocumentType : BaseModelDto
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        public DocumentType(string name)
        {
            Name = name;
        }
    }
}
