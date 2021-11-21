using Microsoft.AspNetCore.Mvc;
using NSI.BusinessLogic.Interfaces;
using NSI.Common.DataContracts.Base;
using NSI.Common.DataContracts.Enumerations;
using NSI.DataContracts.Models;
using NSI.DataContracts.Request;
using NSI.DataContracts.Response;
using NSI.REST.Helpers;
using System;
using System.Threading.Tasks;

namespace NSI.REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : Controller
    {
        private readonly IRolesManipulation _rolesManipulation;

        public RoleController(IRolesManipulation rolesManipulation)
        {
            _rolesManipulation = rolesManipulation;
        }

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

            return new BaseResponse<UserRole>()
            {
                Data = _rolesManipulation.SaveRoleToUser(request.RoleId, request.UserId),
                Error = ValidationHelper.ToErrorResponse(ModelState),
                Success = ResponseStatus.Succeeded
            };
        }

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

            return new BaseDeleteResponse()
            {
                Error = ValidationHelper.ToErrorResponse(ModelState),
                Success = _rolesManipulation.RemoveRoleFromUser(request.RoleId, request.UserId)
            };
        }
    }
}
