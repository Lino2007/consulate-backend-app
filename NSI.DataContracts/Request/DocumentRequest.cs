using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using NSI.Common.DataContracts.Base;
using NSI.Common.Enumerations;

namespace NSI.DataContracts.Request
{
    public class DocumentRequest : BaseRequest
    {
        public RequestType Type { get; set; }

        public string Reason { get; set; }

        public IEnumerable<IFormFile> Attachments { get; set; } = Enumerable.Empty<IFormFile>();

        public string[] AttachmentTypes { get; set; } = { };
    }
}
