using Newtonsoft.Json;
using NSI.Common.DataContracts.Base;

namespace NSI.DataContracts.Models
{
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
