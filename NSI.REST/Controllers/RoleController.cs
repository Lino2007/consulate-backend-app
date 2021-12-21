using Microsoft.AspNetCore.Mvc;
using NSI.BusinessLogic.Interfaces;
using NSI.Common.DataContracts.Base;
using NSI.Common.DataContracts.Enumerations;
using NSI.DataContracts.Models;
using NSI.DataContracts.Request;
using NSI.DataContracts.Response;
using NSI.REST.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSI.REST.Filters;
using Microsoft.AspNetCore.Authorization;
using NSI.Cache.Interfaces;
using NSI.Common.Enumerations;

namespace NSI.REST.Controllers
{
    [ServiceFilter(typeof(CacheCheck))] // Ucitavanje cachea iz baze (ako vec nije), dodavanje korisnika u cache ako je naknadno dodan i fetch permisija korisnika
                                        // (NAJBOLJE staviti ovu anotaciju na razinu kontrolera posto ce nam sve rute biti uglavnom zasticene)
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : Controller
    {
        private readonly IRolesManipulation _rolesManipulation;
        private readonly IUserPermissionManipulation _userPermissionManipulation;
        private readonly ICacheProvider _cacheProvider;

        public RoleController(IRolesManipulation rolesManipulation, IUserPermissionManipulation userPermissionManipulation, ICacheProvider cacheProvider)
        {
            _rolesManipulation = rolesManipulation;
            _userPermissionManipulation = userPermissionManipulation;
            _cacheProvider = cacheProvider;
        }

        /*
        [HttpGet("Test")]
        public async Task<string> Tester()
        {
            // method for testing 
            var test = await _userPermissionManipulation.GetPermissionsForUserAsync("aturkusic1@etf.unsa.ba");
            System.Diagnostics.Debug.WriteLine(test);
            return null;
        }
        */

        /// <summary>
        /// Gets all roles.
        /// </summary>
        [Authorize]
        [PermissionCheck("role:modify")]  // provjera permisija, najbolje staviti ispod authorize-a zbog performansi (ako token ne valja onda mi se ne isplati provjeravati permisije)
        [HttpGet]
        public async Task<RolesResponse> GetRoles([FromQuery] BasicRequest request, [FromQuery(Name = "userId")] Guid userId)
        {
            if (!ModelState.IsValid)
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
                Data = await _rolesManipulation.GetRolesAsync(userId, request.Paging, request.SortCriteria, request.FilterCriteria),
                Error = ValidationHelper.ToErrorResponse(ModelState),
                Success = ResponseStatus.Succeeded
            };
        }

        /// <summary>
        /// Save new role.
        /// </summary>
        [Authorize]
        [PermissionCheck("role:modify")]
        [HttpPost]
        public BaseResponse<Role> SaveRole(NameRequest request)
        {
            if (!ModelState.IsValid)
            {
                return new BaseResponse<Role>()
                {
                    Data = null,
                    Error = ValidationHelper.ToErrorResponse(ModelState),
                    Success = ResponseStatus.Failed
                };
            }

            return new BaseResponse<Role>()
            {
                Data = _rolesManipulation.SaveRole(request.Name),
                Error = ValidationHelper.ToErrorResponse(ModelState),
                Success = ResponseStatus.Succeeded
            };
        }

        /// <summary>
        /// Assign existing role to existing user.
        /// </summary>
        [Authorize]
        [PermissionCheck("role:modify")]
        [HttpPost("user")]
        public BaseResponse<UserRole> SaveRoleToUser(UserRoleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return new BaseResponse<UserRole>()
                {
                    Data = null,
                    Error = ValidationHelper.ToErrorResponse(ModelState),
                    Success = ResponseStatus.Failed
                };
            }

            _cacheProvider.Get<Dictionary<string, List<PermissionEnum>>>("userPermission").Clear();

            return new BaseResponse<UserRole>()
            {
                Data = _rolesManipulation.SaveRoleToUser(request.RoleId, request.UserId),
                Error = ValidationHelper.ToErrorResponse(ModelState),
                Success = ResponseStatus.Succeeded
            };
        }

        /// <summary>
        /// Remove existing role from existing user.
        /// </summary>
        [Authorize]
        [PermissionCheck("role:modify")]
        [HttpDelete("user")]
        public BaseDeleteResponse RemoveRoleFromUser([FromQuery] UserRoleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return new BaseDeleteResponse()
                {
                    Error = ValidationHelper.ToErrorResponse(ModelState),
                    Success = ResponseStatus.Failed
                };
            }

            if (_cacheProvider.Get<Dictionary<string, List<PermissionEnum>>>("userPermission") != null) { 
            _cacheProvider.Get<Dictionary<string, List<PermissionEnum>>>("userPermission").Clear();
            }

            return new BaseDeleteResponse()
            {
                Error = ValidationHelper.ToErrorResponse(ModelState),
                Success = _rolesManipulation.RemoveRoleFromUser(request.RoleId, request.UserId)
            };
        }
    }
}
