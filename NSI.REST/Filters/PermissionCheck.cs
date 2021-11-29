using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NSI.Common.Enumerations;

namespace NSI.REST.Filters
{
    public class PermissionCheck : ActionFilterAttribute
    {
        public string[] routePermissions;

        public PermissionCheck(params string[] list)
        {
            this.routePermissions = list;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            List<PermissionEnum> routePerm = new List<PermissionEnum>();
            if (!ValidateRoutePermissionList(routePerm))
            {
                context.Result = new StatusCodeResult(555); // ako je typo u listi permisija
                return;
            }

            if (routePerm.Count == 0) return;

            var userPermissions = (List<PermissionEnum>) context.HttpContext.Items["permissions"];

            if (userPermissions != null)
            {
                if (!IsAuthorized(userPermissions, routePerm))
                {
                   context.Result = new StatusCodeResult(401);
                   return;
                }

            }
            else if (userPermissions == null && routePerm.Count > 0)
            {
                context.Result = new StatusCodeResult(401);
                return;
            }
        }

        private bool IsAuthorized(List<PermissionEnum> userPermissions, List<PermissionEnum> routePerm)
        {
            foreach (var permission in routePerm)
            {
                var index = userPermissions.IndexOf(permission);
                if (index == -1) // korisnik nema trazenu permisiju
                {
                    return false;
                }
            }
            return true;
        }

        private bool ValidateRoutePermissionList(List<PermissionEnum> routePerm)
        {
            foreach (var permission in this.routePermissions)
            {
                var permItem = PermissionEnumExtension.GetEnumByPermissionName(permission);
                if (permItem == PermissionEnum.None) // ako je typo u listi permisija
                {
                    return false;
                }
                routePerm.Add(permItem);
            }
            return true;
        }
    }
}
