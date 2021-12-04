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

namespace NSI.REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {

        private readonly IAuthManipulation _authManipulation;

        private readonly IUsersManipulation _usersManipulation;

        public UserController(IAuthManipulation authManipulation, IUsersManipulation usersManipulation)
        {
            _authManipulation = authManipulation;
            _usersManipulation = usersManipulation;
        }

        /// <summary>
        /// Get Role from loged user by email.
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

            return new BaseResponse<User>()
            {
                Data = _usersManipulation.saveUser(request),
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

    }
}
