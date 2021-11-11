using Microsoft.Extensions.Configuration;
using NSI.DataContracts.Models;
using NSI.Proxy.Azure;
using NSI.Proxy.Azure.Request;
using NSI.Proxy.Azure.Response;
using NSI.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Implementations
{
    public class WorkItemsRepository : IWorkItemsRepository
    {
        private readonly IAzureProxy _proxy;
        private readonly IConfiguration _configuration;
        private string _endpointUrl { get; set; }
        private string _organization { get; set; }

        public WorkItemsRepository(
            IAzureProxy azureProxy,
            IConfiguration configuration)
        {
            _proxy = azureProxy;
            _configuration = configuration;
            _endpointUrl = configuration["BaseAzureEndpoint"];
            _endpointUrl = configuration["AzureOrganization"];
        }

        public async Task<IList<WorkItemDto>> GetWorkItemsAsync(IList<int> itemIds)
        {
            GetWorkItemsBatchRequest request = new GetWorkItemsBatchRequest
            {
                Ids = itemIds,
                Fields = _configuration["WorkItemsFields"].Split(",").ToList()
            };

            var data = await _proxy.Post<GetWorkItemsBatchResponse>(GetWorkItemsBatchUrl(), request);
            return data?.Value?.Select(x => x.Fields).ToList();
        }

        private string GetWorkItemsBatchUrl()
        {
            return string.Format(_endpointUrl, _organization, "wit/workitemsbatch");
        }
    }
}
