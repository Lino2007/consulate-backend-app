using NSI.DataContracts.Models;

namespace NSI.Repository.Interfaces
{
    public interface IRequestsRepository
    {
        Request SaveRequest(Request request);
    }
}
