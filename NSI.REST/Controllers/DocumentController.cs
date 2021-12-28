using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSI.BusinessLogic.Interfaces;
using NSI.Common.DataContracts.Base;
using NSI.Common.DataContracts.Enumerations;
using NSI.DataContracts.Models;
using NSI.REST.Filters;
using NSI.REST.Helpers;

namespace NSI.REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : Controller
    {
        private readonly IDocumentsManipulation _documentsManipulation;

        public DocumentController(IDocumentsManipulation documentsManipulation)
        {
            _documentsManipulation = documentsManipulation;
        }

        /// <summary>
        /// Gets document by id, if expiration date has not passed.
        /// </summary>
        // [Authorize]
        // [PermissionCheck("document:view")]
        [HttpGet("{id}")]
        public BaseResponse<Document> GetDocumentIfNotExpired([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return new BaseResponse<Document>
                {
                    Data = null,
                    Error = ValidationHelper.ToErrorResponse(ModelState),
                    Success = ResponseStatus.Failed
                };
            }

            return new BaseResponse<Document>
            {
                Data = _documentsManipulation.GetDocumentIfNotExpired(id),
                Error = ValidationHelper.ToErrorResponse(ModelState),
                Success = ResponseStatus.Succeeded
            };
        }
    }
}
