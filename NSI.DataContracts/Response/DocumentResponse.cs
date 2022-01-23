using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NSI.Common.Collation;
using NSI.Common.Collation.Interfaces;
using NSI.Common.DataContracts.Base;
using NSI.DataContracts.Models;

namespace NSI.DataContracts.Response
{
    [ExcludeFromCodeCoverage]
    public class DocumentResponse: BaseResponse<IList<Document>>, IPageable
    {
        public IList<Document> Documents { get; set; }
        public Paging Paging { get; set; }
    }
}
