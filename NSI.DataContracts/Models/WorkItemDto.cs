using Newtonsoft.Json;
using NSI.Common.DataContracts.Base;

namespace NSI.DataContracts.Models
{
    public class WorkItemDto : BaseModelDto
    {
        public new int? Id { get; set; }

        [JsonProperty(PropertyName = "System.Title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "System.Description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "System.WorkItemType")]
        public string WorkItemType { get; set; }

        [JsonProperty(PropertyName = "System.State")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "System.AreaPath")]
        public string AreaPath { get; set; }

        [JsonProperty(PropertyName = "System.Priority")]
        public int? Priority { get; set; }

        [JsonProperty(PropertyName = "Custom.Release")]
        public string Release { get; set; }

        [JsonProperty(PropertyName = "Microsoft.VSTS.Common.BacklogPriority")]
        public decimal BacklogPriority { get; set; }

        public bool IsTopLevelItem
        {
            get
            {
                return WorkItemType == "Product Backlog Item" || WorkItemType == "Bug";
            }
        }

        [JsonProperty("id")]
        public int AlternateIdField
        {
            get => Id ?? 1;
            set { if (value > 0) Id = value; }
        }
    }
}
