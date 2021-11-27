using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSI.BusinessLogic.Interfaces;
using NSI.Common.DataContracts.Base;
using NSI.Common.DataContracts.Enumerations;
using NSI.DataContracts.Models;
using NSI.REST.Helpers;

namespace NSI.REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthManipulation _authManipulation;

        public AuthController(IAuthManipulation authManipulation)
        {
            _authManipulation = authManipulation;
        }
        
        /// <summary>
        /// Gets user information by email.
        /// </summary>
        [Authorize]
        [HttpGet]
        public BaseResponse<User> GetUserInformation([FromQuery(Name = "email")] string email)
        {
            if (!ModelState.IsValid)
            {
                return new BaseResponse<User>
                {
                    Data = null,
                    Error = ValidationHelper.ToErrorResponse(ModelState),
                    Success = ResponseStatus.Failed
                };
            }

            return new BaseResponse<User>
            {
                Data = _authManipulation.GetByEmail(email),
                Error = ValidationHelper.ToErrorResponse(ModelState),
                Success = ResponseStatus.Succeeded
            };
        }
    }
}
