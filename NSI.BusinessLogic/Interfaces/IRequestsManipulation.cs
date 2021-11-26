using System;
using NSI.Common.Enumerations;
using NSI.DataContracts.Models;

namespace NSI.BusinessLogic.Interfaces
{
    public interface IRequestsManipulation
    {
        Request SaveRequest(Guid userId, string requestReason, RequestType requestType);
    }
}
