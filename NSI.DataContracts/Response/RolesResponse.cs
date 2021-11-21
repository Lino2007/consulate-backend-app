using NSI.Common.Collation;
using NSI.Common.Collation.Interfaces;
using NSI.Common.DataContracts.Base;
using NSI.DataContracts.Models;
using System.Collections.Generic;

namespace NSI.DataContracts.Response
{
    public class RolesResponse : BaseResponse<IList<Role>>, IPageable
    {
        public IList<Role> Roles { get; set; }
        public Paging Paging { get; set; }
    }
}
