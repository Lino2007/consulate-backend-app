using Newtonsoft.Json;
using NSI.Common.DataContracts.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NSI.DataContracts.Models
{
    public class UserRole : BaseModelDto
    {
        [JsonProperty(PropertyName = "userId")]
        public Guid UserId { get; set; }

        [JsonProperty(PropertyName = "roleId")]
        public Guid RoleId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        public UserRole(Guid userId, Guid roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }
    }
}
