using NSI.Common.DataContracts.Base;
using System;

namespace NSI.DataContracts.Request
{
    public class RolePermissionRequest : BaseRequest
    {
        public Guid RoleId { get; set; }

        public Guid PermissionId { get; set; }
    }
}
