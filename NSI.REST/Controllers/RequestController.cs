using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using NSI.BusinessLogic.Interfaces;
using NSI.Common.DataContracts.Base;
using NSI.Common.DataContracts.Enumerations;
using NSI.DataContracts.Models;
using NSI.DataContracts.Request;
using NSI.DataContracts.Response;
using NSI.REST.Filters;
using NSI.REST.Helpers;
using NSI.Common.Enumerations;
using NSI.Common.Utilities;

namespace NSI.REST.Controllers
{
    [ServiceFilter(typeof(CacheCheck))]
    [ApiController]
    [Route("api/[controller]")]
    public class RequestController : Controller
    {
        private readonly IRequestsManipulation _requestsManipulation;
        private readonly IUsersManipulation _usersManipulation;
        private readonly IPdfManipulation _pdfManipulation;
        private readonly IDocumentsManipulation _documentsManipulation;
        private readonly IDocumentTypesManipulation _documentTypesManipulation;
        private readonly IFilesManipulation _filesManipulation;
        private readonly string _redirectUrl;

        public RequestController(IRequestsManipulation requestsManipulation,
            IUsersManipulation usersManipulation, IPdfManipulation pdfManipulation,
            IDocumentsManipulation documentsManipulation, IDocumentTypesManipulation documentTypesManipulation,
            IFilesManipulation filesManipulation, IConfiguration configuration)
        {
            _requestsManipulation = requestsManipulation;
            _usersManipulation = usersManipulation;
            _pdfManipulation = pdfManipulation;
            _documentsManipulation = documentsManipulation;
            _documentTypesManipulation = documentTypesManipulation;
            _filesManipulation = filesManipulation;
            _redirectUrl = configuration["QRCodeRedirectionUrl"];
        }

        /// <summary>
        /// Save new request.
        /// </summary>
        [Authorize]
        [PermissionCheck("request:create")]
        [HttpPost]
        public async Task<BaseResponse<Request>> SaveRequest([FromForm] DocumentRequest request)
        {
            if (!ModelState.IsValid)
            {
                return new BaseResponse<Request>()
                {
                    Data = null,
                    Error = ValidationHelper.ToErrorResponse(ModelState),
                    Success = ResponseStatus.Failed
                };
            }

            return new BaseResponse<Request>()
            {
                Data = await _requestsManipulation.SaveRequest(
                    _usersManipulation.GetByEmail(AuthHelper.GetRequestEmail(HttpContext)).Id,
                    request.Reason,
                    request.Type,
                    request.Attachments,
                    request.AttachmentTypes
                ),
                Error = ValidationHelper.ToErrorResponse(ModelState),
                Success = ResponseStatus.Succeeded
            };
        }

        /// <summary>
        /// Get requests.
        /// </summary>
        [Authorize]
        [PermissionCheck("request:view")]
        [HttpGet]
        public async Task<ReqResponse> GetRequests([FromQuery] BasicRequest basicRequest)
        {
            if (!ModelState.IsValid)
            {
                return new ReqResponse()
                {
                    Data = null,
                    Error = ValidationHelper.ToErrorResponse(ModelState),
                    Success = ResponseStatus.Failed
                };
            }

            return new ReqResponse()
            {
                Data = await _requestsManipulation.GetRequestsAsync(),
                Error = ValidationHelper.ToErrorResponse(ModelState),
                Success = ResponseStatus.Succeeded
            };
        }

        /// <summary>
        /// Get requests with paging.
        /// </summary>
        [Authorize]
        [PermissionCheck("request:view")]
        [HttpGet("paging")]
        public async Task<ReqItemListResponse> GetRequestsWithPaging([FromQuery] BasicRequest basicRequest)
        {
            if (!ModelState.IsValid || basicRequest.Paging == null)
            {
                return new ReqItemListResponse()
                {
                    Data = null,
                    Error = ValidationHelper.ToErrorResponse(ModelState),
                    Success = ResponseStatus.Failed
                };
            }

            if (basicRequest.Paging.RecordsPerPage < 0)
            {
                basicRequest.Paging.RecordsPerPage = 5;
            }

            if (basicRequest.Paging.Page < 0)
            {
                basicRequest.Paging.Page = 0;
            }

            return new ReqItemListResponse()
            {
                Paging = basicRequest.Paging,
                Data = await _requestsManipulation.GetRequestPage(basicRequest.Paging),
                Error = ValidationHelper.ToErrorResponse(ModelState),
                Success = ResponseStatus.Succeeded
            };
        }

        /// <summary>
        /// Get requests by employee id.
        /// </summary>
        [Authorize]
        [PermissionCheck("request:view")]
        [HttpGet("employee/{id}")]
        public async Task<ReqItemListResponse> GetRequestsByEmployeeId(string id, [FromQuery] BasicRequest basicRequest)
        {
            if (!ModelState.IsValid || basicRequest.Paging == null)
            {
                return new ReqItemListResponse()
                {
                    Data = null,
                    Error = ValidationHelper.ToErrorResponse(ModelState),
                    Success = ResponseStatus.Failed
                };
            }

            if (basicRequest.Paging.RecordsPerPage < 0)
            {
                basicRequest.Paging.RecordsPerPage = 5;
            }

            if (basicRequest.Paging.Page < 0)
            {
                basicRequest.Paging.Page = 0;
            }

            return new ReqItemListResponse()
            {
                Paging = basicRequest.Paging,
                Data = await _requestsManipulation.GetEmployeeRequestsAsync(id, basicRequest.Paging),
                Error = ValidationHelper.ToErrorResponse(ModelState),
                Success = ResponseStatus.Succeeded
            };
        }

        /// <summary>
        /// Update request state, generate document if request is approved.
        /// </summary>
        [Authorize]
        [PermissionCheck("document:create")]
        [HttpPut]
        public async Task<ReqResponse> UpdateRequest(ReqItemRequest req)
        {
            if (!ModelState.IsValid)
            {
                return new ReqResponse()
                {
                    Data = null,
                    Error = ValidationHelper.ToErrorResponse(ModelState),
                    Success = ResponseStatus.Failed
                };
            }

            User user = _usersManipulation.GetByEmail(AuthHelper.GetRequestEmail(HttpContext));
            var request = await _requestsManipulation.UpdateRequestAsync(req, user);
            if (request == null)
            {
                Error newErr = new Error();
                newErr.Message = $"Request with id {req.Id} does not exist";
                return new ReqResponse()
                {
                    Data = null,
                    Error = newErr,
                    Success = ResponseStatus.Failed
                };
            }

            if (request.State.Equals(RequestState.Approved))
            {
                DocumentType documentType = _documentTypesManipulation.GetByName(request.Type.ToString());
                Document document = _documentsManipulation.SaveDocument(request.Id, documentType.Id,
                    DateTime.UtcNow.AddYears(10), null, null);
                User requestUser = _usersManipulation.GetById(request.UserId);

                var content = $"{_redirectUrl}/api/Document/{document.Id}";
                var bitmap = QRCodeHelper.GenerateBitmap(content);
                var streamImg = new MemoryStream();
                bitmap.Save(streamImg, ImageFormat.Png);
                var imageBytes = ImageToByte(bitmap);
                IFormFile fileImage = new FormFile(streamImg, 0, imageBytes.Length, "document", document.Id + ".png");
                var qrImageUrl = await _filesManipulation.UploadFile(fileImage, document.Id.ToString());
                var qrImageBase64 = "data:image/png;base64," + Convert.ToBase64String(imageBytes);

                byte[] fileBytes;
                if (request.Type.Equals(RequestType.Passport))
                {
                    fileBytes = _pdfManipulation.CreatePassportPdf(document, requestUser, qrImageBase64, qrImageUrl);
                }
                else
                {
                    fileBytes = _pdfManipulation.CreateVisaPdf(document, requestUser, qrImageBase64, qrImageUrl);
                }

                var stream = new MemoryStream(fileBytes);
                IFormFile file = new FormFile(stream, 0, fileBytes.Length, "document", document.Id + ".pdf");
                string url = await _filesManipulation.UploadFile(file, document.Id.ToString());
                document.Title = documentType.Name + " - " + requestUser.FirstName + " " + requestUser.LastName;
                document.Url = url;
                _documentsManipulation.UpdateDocument(document);
            }

            return new ReqResponse
            {
                Data = new List<Request> {request},
                Error = ValidationHelper.ToErrorResponse(ModelState),
                Success = ResponseStatus.Succeeded
            };
        }

        private static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[]) converter.ConvertTo(img, typeof(byte[]));
        }
    }
}
