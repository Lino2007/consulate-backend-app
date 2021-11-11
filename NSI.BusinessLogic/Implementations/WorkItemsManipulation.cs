using NSI.BusinessLogic.Interfaces;
using NSI.Common.Collation;
using NSI.DataContracts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NSI.Repository.Interfaces;
using NSI.Common.Extensions;
using System.Linq;

namespace NSI.BusinessLogic.Implementations
{
    public class WorkItemsManipulation : IWorkItemsManipulation
    {
        private readonly IWorkItemsRepository _workItemsRepository;

        public WorkItemsManipulation(IWorkItemsRepository workItemsRepository)
        {
            _workItemsRepository = workItemsRepository;
        }

        public async Task<IList<WorkItemDto>> GetWorkItemsAsync(IList<int> itemIds, Paging paging, IList<SortCriteria> sortCriteria, IList<FilterCriteria> filterCriteria)
        {
            var results = await _workItemsRepository.GetWorkItemsAsync(itemIds);

            if (paging != null)
            {
                results = results.AsQueryable().DoPaging(paging).ToList();
            }

            return results;
        }
    }
}
