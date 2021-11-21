using NSI.Common.DataContracts.Base;
using System;

namespace NSI.DataContracts.Request
{
    public class UserRoleRequest : BaseRequest
    {
        public Guid UserId { get; set; }

        public Guid RoleId { get; set; }
    }
}
