using Newtonsoft.Json;
using NSI.Common.DataContracts.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace NSI.DataContracts.Models
{
    [Table("Role", Schema = "echo")]
    public class Role : BaseModelDto
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        public Role(string name)
        {
            Name = name;
        }
    }
}
