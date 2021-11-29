using Microsoft.AspNetCore.Mvc.Filters;
using NSI.BusinessLogic.Interfaces;
using NSI.Cache.Interfaces;
using NSI.Common.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSI.Common.Enumerations;

namespace NSI.REST.Filters
{
    public class CacheCheck : IAsyncActionFilter
    {
        private readonly ICacheProvider _cacheProvider;
        private readonly IUserPermissionManipulation _userPermissionManipulation;
        private Dictionary<string, List<PermissionEnum>> userPermission = new Dictionary<string, List<PermissionEnum>>();

        public CacheCheck(IUserPermissionManipulation userPermissionManipulation, ICacheProvider cacheProvider)
        {
            _userPermissionManipulation = userPermissionManipulation;
            _cacheProvider = cacheProvider;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var email = LoadEmailFromToken(context);
            if (email != null)
            {
                LoadPermissionsToCache();
                await AddUserToCacheIfNotExist(email);
                context.HttpContext.Items["permissions"] = GetUserPermissions(email);
            }
            await next();
        }
    
        private void LoadPermissionsToCache()
        {
            var temp = _cacheProvider.Get<Dictionary<string, List<PermissionEnum>>>("userPermission");
            if (temp == null)
            {
                
                this.userPermission = new Dictionary<string, List<PermissionEnum>>();
                _cacheProvider.Set("userPermission", this.userPermission);
            }
            else
            {
                this.userPermission = temp;
            }
        }

        private string LoadEmailFromToken(ActionExecutingContext context)
        {
            string jwtToken = null;
            var temp = context.HttpContext.Request.Headers["Authorization"];
           
            if (temp.Count == 0) return null;
            jwtToken = temp.ToString().Replace("Bearer ", "");

            return JwtHelper.GetMailFromJwtToken(jwtToken); 
        }

        private async Task AddUserToCacheIfNotExist(string email)
        {
            if (!userPermission.ContainsKey(email))
            {
                this.userPermission.Add(email, await _userPermissionManipulation.GetPermissionsForUserAsync(email));
            }
        }

       private List<PermissionEnum> GetUserPermissions(string email)
        {
            return this.userPermission[email];
        }
    }
}
