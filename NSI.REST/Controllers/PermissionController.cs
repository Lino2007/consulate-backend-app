using Microsoft.AspNetCore.Mvc;
using NSI.BusinessLogic.Interfaces;
using NSI.Common.DataContracts.Base;
using NSI.Common.DataContracts.Enumerations;
using NSI.DataContracts.Models;
using NSI.DataContracts.Request;
using NSI.DataContracts.Response;
using NSI.REST.Helpers;
using System.Threading.Tasks;

namespace NSI.REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionController : Controller
    {

        private readonly IPermissionsManipulation _permissionsManipulation;

        public PermissionController(IPermissionsManipulation permissionsManipulation)
        {
            _permissionsManipulation = permissionsManipulation;
        }

        [HttpGet]
        public async Task<PermissionsResponse> GetPermissions([FromQuery] BasicRequest request, [FromQuery(Name = "role")] string role)
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
                Data = await _permissionsManipulation.GetPermissionsAsync(role, request.Paging, request.SortCriteria, request.FilterCriteria),
                Error = ValidationHelper.ToErrorResponse(ModelState),
                Success = ResponseStatus.Succeeded
            };
        }

        [HttpPost]
        public BaseResponse<Permission> SavePermission(NameRequest request)
        {
            if (!ModelState.IsValid)
            {
                return new BaseResponse<Permission>()
                {
                    Data = null,
                    Error = ValidationHelper.ToErrorResponse(ModelState),
                    Success = ResponseStatus.Failed
                };
            }

            return new BaseResponse<Permission>()
            {
                Data = _permissionsManipulation.SavePermission(request.Name),
                Error = ValidationHelper.ToErrorResponse(ModelState),
                Success = ResponseStatus.Succeeded
            };
        }

        [HttpPost("role")]
        public BaseResponse<RolePermission> SavePermissionToRole(RolePermissionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return new BaseResponse<RolePermission>()
                {
                    Data = null,
                    Error = ValidationHelper.ToErrorResponse(ModelState),
                    Success = ResponseStatus.Failed
                };
            }

            return new BaseResponse<RolePermission>()
            {
                Data = _permissionsManipulation.SavePermissionToRole(request.PermissionId, request.RoleId),
                Error = ValidationHelper.ToErrorResponse(ModelState),
                Success = ResponseStatus.Succeeded
            };
        }

        [HttpDelete("role")]
        public BaseDeleteResponse RemovePermissionFromRole([FromQuery] RolePermissionRequest request)
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
                Success = _permissionsManipulation.RemovePermissionFromRole(request.PermissionId, request.RoleId)
            };
        }
    }
}
