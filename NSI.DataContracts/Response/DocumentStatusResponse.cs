using NSI.Common.Enumerations;
using NSI.DataContracts.Models;

namespace NSI.DataContracts.Response
{
    public class DocumentStatusResponse
    {
        public Document Document { get; set; }
        public DocumentStatus Status { get; set; }

        public DocumentStatusResponse(Document document, DocumentStatus status)
        {
            Document = document;
            Status = status;
        }
    }
}
