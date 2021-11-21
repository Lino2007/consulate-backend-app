using Newtonsoft.Json;
using NSI.Common.DataContracts.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NSI.DataContracts.Models
{
    public class RolePermission : BaseModelDto
    {
        [JsonProperty(PropertyName = "roleId")]
        public Guid RoleId { get; set; }

        [JsonProperty(PropertyName = "permissionId")]
        public Guid PermissionId { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        [ForeignKey("PermissionId")]
        public Permission Permission { get; set; }

        public RolePermission(Guid roleId, Guid permissionId)
        {
            RoleId = roleId;
            PermissionId = permissionId;
        }
    }
}
