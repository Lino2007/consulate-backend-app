using NSI.Common.Collation;
using NSI.Common.Collation.Interfaces;
using NSI.Common.DataContracts.Base;
using NSI.DataContracts.Models;
using System.Collections.Generic;

namespace NSI.DataContracts.Response
{
    public class PermissionsResponse : BaseResponse<IList<Permission>>, IPageable
    {
        public IList<Permission> Permissions { get; set; }
        public Paging Paging { get; set; }
    }
}
