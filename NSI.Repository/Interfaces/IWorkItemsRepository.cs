using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NSI.DataContracts.Models;
namespace NSI.Repository.Interfaces
{
    public interface IWorkItemsRepository
    {
        Task<IList<WorkItemDto>> GetWorkItemsAsync(IList<int> itemIds);
    }
}
