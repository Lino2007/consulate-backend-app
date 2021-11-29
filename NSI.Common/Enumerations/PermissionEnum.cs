using System;
using System.Collections.Generic;

namespace NSI.Common.Enumerations
{
    public enum PermissionEnum
    {
        None, RoleModify, PerModify,
        EmpCreate, EmpDelete, EmpUpdate,
        UsrDelete, DocCreate, DocView,
        ReqView, ReqCreate, ProView
    }

    public static class PermissionEnumExtension {

        private static readonly List<string> enumNames = new List<string>
        {
            "role:modify", "permission:modify", "employee:create", "employee:delete", "employee:update",
            "user:delete", "document:create", "document:view", "request:view", "request:create", "profile:view"
        };

        public static PermissionEnum GetEnumByPermissionName(string permissionName)
        {
            int result = enumNames.IndexOf(permissionName);
            return result == -1 ? PermissionEnum.None : (PermissionEnum) result + 1;
        }

        public static String GetPermissionNameFromEnum(PermissionEnum permissionName)
        {
            int index = (int)(object)permissionName;
            return enumNames[index - 1];
        }
    }
}
