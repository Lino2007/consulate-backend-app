using NSI.Common.DataContracts.Enumerations;

namespace NSI.Common.DataContracts.Base
{
    public class BaseDeleteResponse
    {
        public Error Error { get; set; }

        public ResponseStatus Success { get; set; }
    }
}
