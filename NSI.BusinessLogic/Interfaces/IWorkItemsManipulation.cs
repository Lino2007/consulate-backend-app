using NSI.Common.Collation;
using NSI.DataContracts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NSI.BusinessLogic.Interfaces
{
    public interface IWorkItemsManipulation
    {
        Task<IList<WorkItemDto>> GetWorkItemsAsync(IList<int> itemIds, Paging paging, IList<SortCriteria> sortCriteria, IList<FilterCriteria> filterCriteria);
    }
}
