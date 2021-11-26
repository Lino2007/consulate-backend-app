using NSI.Common.DataContracts.Base;
using NSI.Common.Enumerations;

namespace NSI.DataContracts.Request
{
    public class DocumentRequest : BaseRequest
    {
        public RequestType Type { get; set; }

        public string Reason { get; set; }
    }
}
