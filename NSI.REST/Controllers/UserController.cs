using Microsoft.AspNetCore.Mvc;
using NSI.BusinessLogic.Interfaces;
using NSI.DataContracts.Models;
using System.ComponentModel.DataAnnotations;
using NSI.Common.DataContracts.Base;
using NSI.REST.Helpers;
using NSI.Common.DataContracts.Enumerations;
using NSI.DataContracts.Request;
using Microsoft.AspNetCore.Authorization;
using NSI.DataContracts.Response;
using System.Threading.Tasks;
using NSI.REST.Filters;
using NSI.Cache.Interfaces;
using System.Collections.Generic;
using NSI.Common.Enumerations;

namespace NSI.REST.Controllers
{
    [ServiceFilter(typeof(CacheCheck))]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {

        private readonly IAuthManipulation _authManipulation;

        private readonly IUsersManipulation _usersManipulation;

        private readonly IPermissionsManipulation _permissionsManipulation;
        
        private readonly IDocumentsManipulation _documentsManipulation;

        private readonly ICacheProvider _cacheProvider;

        public UserController(IAuthManipulation authManipulation, IUsersManipulation usersManipulation, IPermissionsManipulation permissionsManipulation, IDocumentsManipulation documentsManipulation, ICacheProvider cacheProvider)
        {
            _authManipulation = authManipulation;
            _usersManipulation = usersManipulation;
            _permissionsManipulation = permissionsManipulation;
            _documentsManipulation = documentsManipulation;
            _cacheProvider = cacheProvider;
        }

        /// <summary>
        /// Get Role from logged user by email.
        /// </summary>
        [HttpGet]
        public BaseResponse<Role> GetUserRoleFromEmail([FromQuery(Name = "email")] string email)
        {
            if (!ModelState.IsValid || email == null || !new EmailAddressAttribute().IsValid(email)) 
            {
                return new BaseResponse<Role>
                {
                    Data = null,
                    Error = ValidationHelper.ToErrorResponse(ModelState),
                    Success = ResponseStatus.Failed
                };
            }

            return new BaseResponse<Role>
            {
                Data = _authManipulation.GetRoleFromEmail(email),
                Error = ValidationHelper.ToErrorResponse(ModelState),
                Success = ResponseStatus.Succeeded
            };
        }

        /// <summary>
        /// Add new unregistered user to our db.
        /// </summary>
        [Authorize]
        [HttpPost]
        public BaseResponse<User> SaveNewUser(NewUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return new BaseResponse<User>()
                {
                    Data = null,
                    Error = ValidationHelper.ToErrorResponse(ModelState),
                    Success = ResponseStatus.Failed
                };
            }

            if (!new EmailAddressAttribute().IsValid(request.Email) || request.Email == null ||
                request.FirstName == null || request.LastName == null || request.Username == null ||
                request.PlaceOfBirth == null || request.Country == null) 
            {
                return new BaseResponse<User>()
                {
                    Data = null,
                    Error = ValidationHelper.ToErrorResponse(ModelState),
                    Success = ResponseStatus.Failed
                };
            }

           if (_cacheProvider.Get<Dictionary<string, List<PermissionEnum>>>("userPermission") != null)
           {
                _cacheProvider.Get<Dictionary<string, List<PermissionEnum>>>("userPermission").Clear();
           }

            return new BaseResponse<User>()
            {
                Data = _usersManipulation.SaveUser(request),
                Error = ValidationHelper.ToErrorResponse(ModelState),
                Success = ResponseStatus.Succeeded
            };
        }

        /// <summary>
        /// Delete current user profile.
        /// </summary>
        [Authorize]
        [HttpDelete]
        public BaseDeleteResponse RemoveUser([FromQuery(Name = "email")] string email)
        {
            if (!ModelState.IsValid || email == null || !new EmailAddressAttribute().IsValid(email))
            {
                return new BaseDeleteResponse()
                {
                    Error = ValidationHelper.ToErrorResponse(ModelState),
                    Success = ResponseStatus.Failed
                };
            }

            return new BaseDeleteResponse()
            {
                Error = ValidationHelper.ToErrorResponse(ModelState),
                Success = _usersManipulation.RemoveUser(email)
            };
        }

        /// <summary>
        /// Get all population.
        /// </summary>
        [Authorize]
        [HttpGet]
        [Route("users")]
        public async Task<UserResponse> GetUsers([FromQuery] BasicRequest request)
        {
            if (!ModelState.IsValid)
            {
                return new UserResponse()
                {
                    Data = null,
                    Error = ValidationHelper.ToErrorResponse(ModelState),
                    Success = ResponseStatus.Failed
                };
            }

            return new UserResponse()
            {
                Data = await _usersManipulation.GetUsers(request.Paging, request.SortCriteria, request.FilterCriteria),
                Error = ValidationHelper.ToErrorResponse(ModelState),
                Success = ResponseStatus.Succeeded
            };
        }
        
        /// <summary>
        /// Gets all permissions from user.
        /// </summary>
        [Authorize]
        [HttpGet("permission")]
        public async Task<PermissionsResponse> GetPermissionsByUserId()
        {
            if (!ModelState.IsValid)
            {
                return new PermissionsResponse()
                {
                    Data = null,
                    Error = ValidationHelper.ToErrorResponse(ModelState),
                    Success = ResponseStatus.Failed
                };
            }

            return new PermissionsResponse()
            {
                Data = await _permissionsManipulation.GetPermissionsByUserId(_usersManipulation.GetByEmail(AuthHelper.GetRequestEmail(HttpContext)).Id),
                Error = ValidationHelper.ToErrorResponse(ModelState),
                Success = ResponseStatus.Succeeded
            };
        }
        
        /// <summary>
        /// Gets all documents from user.
        /// </summary>
        [Authorize]
        [PermissionCheck("document:view")]
        [HttpGet("document")]
        public async Task<DocumentResponse> GetDocumentsByUserIdAndType([FromQuery(Name = "type")] string type)
        {
            if (!ModelState.IsValid)
            {
                return new DocumentResponse()
                {
                    Data = null,
                    Error = ValidationHelper.ToErrorResponse(ModelState),
                    Success = ResponseStatus.Failed
                };
            }

            return new DocumentResponse()
            {
                Data = await _documentsManipulation.GetDocumentsByUserIdAndType(_usersManipulation.GetByEmail(AuthHelper.GetRequestEmail(HttpContext)).Id, type),
                Error = ValidationHelper.ToErrorResponse(ModelState),
                Success = ResponseStatus.Succeeded
            };
        }

        /// <summary>
        /// Get all roles from logged user by email.
        /// </summary>
        [Authorize]
        [HttpGet]
        [Route("roles")]
        public async Task<RolesResponse> GetUserRolesFromEmail([FromQuery(Name = "email")] string email)
        {
            if (!ModelState.IsValid || email == null || !new EmailAddressAttribute().IsValid(email))
            {
                return new RolesResponse()
                {
                    Data = null,
                    Error = ValidationHelper.ToErrorResponse(ModelState),
                    Success = ResponseStatus.Failed
                };
            }

            return new RolesResponse()
            {
                Data = await _authManipulation.GetRoles(email),
                Error = ValidationHelper.ToErrorResponse(ModelState),
                Success = ResponseStatus.Succeeded
            };
        }
    }
}
