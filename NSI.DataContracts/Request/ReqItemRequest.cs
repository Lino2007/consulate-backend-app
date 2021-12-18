using NSI.Common.Enumerations;
using NSI.Common.DataContracts.Base;

namespace NSI.DataContracts.Request
{
    public class ReqItemRequest : BaseRequest
    {
        public string Id { get; set; }
        public RequestState RequestState { get; set; }
    }
}
