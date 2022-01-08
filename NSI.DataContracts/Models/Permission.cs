using Newtonsoft.Json;
using NSI.Common.DataContracts.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace NSI.DataContracts.Models
{
    [Table("Permission", Schema = "echo")]
    public class Permission : BaseModelDto
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        public Permission(string name)
        {
            Name = name;
        }
    }
}
