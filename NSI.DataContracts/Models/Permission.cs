using Newtonsoft.Json;
using NSI.Common.DataContracts.Base;

namespace NSI.DataContracts.Models
{
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
