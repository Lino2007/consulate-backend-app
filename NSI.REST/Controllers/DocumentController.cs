using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSI.BusinessLogic.Interfaces;
using NSI.Common.Enumerations;
using NSI.Common.Utilities;
using NSI.REST.Filters;

namespace NSI.REST.Controllers
{
    [ApiController]
    [ServiceFilter(typeof(CacheCheck))]
    [Route("api/[controller]")]
    public class DocumentController : Controller
    {
        private readonly IDocumentsManipulation _documentsManipulation;

        public DocumentController(IDocumentsManipulation documentsManipulation)
        {
            _documentsManipulation = documentsManipulation;
        }

        /// <summary>
        /// Get HTML validation page for document by id.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ContentResult> GetDocumentIfNotExpired([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return new ContentResult
                {
                    ContentType = "text/html",
                    StatusCode = (int) HttpStatusCode.BadRequest,
                    Content = "<html> " +
                              "<head> <meta charset=\"UTF-8\"> </head> " +
                              "<body> " +
                              "<div " +
                              "style=\"top: 50%; left: 50%; transform: translate(-50% , -50%); " +
                              "position: absolute; background: lightcoral; font-size: 30px; font-family: Arial;\">" +
                              "<h3 style=\"margin: 50px;\">Oops! Something went wrong.</h3> " +
                              "<h5 style=\"margin: 50px; color: gray;\">Check if URL and document ID is valid.</h5> " +
                              "</div> " +
                              "</body> " +
                              "</html>"
                };
            }

            var documentWithStatus = await _documentsManipulation.GetDocumentWithStatus(id);
            var document = documentWithStatus.Document;
            var status = documentWithStatus.Status;
            
            if (status == DocumentStatus.Valid)
            {
                return new ContentResult
                {
                    ContentType = "text/html",
                    StatusCode = (int) HttpStatusCode.OK,
                    Content = "<html> " +
                              "<head> <meta charset=\"UTF-8\"> </head> " +
                              "<body> " +
                              "<div " +
                              "style=\"top: 50%; left: 50%; transform: translate(-50% , -50%); " +
                              "position: absolute; background: lightgreen; font-size: 30px; font-family: Arial;\">" +
                              "<h1 style=\"margin: 50px;\">Document is VALID.</h1> " +
                              "<h5 style=\"margin: 50px; color: gray;\">Title: " + document.Title + "</h5>" +
                              "<h5 style=\"margin: 50px; color: gray;\">Expiration Date: " + DateHelper.ConvertToLocalTimeZone(document.DateOfExpiration) +
                              "</h5>" +
                              "</div> " +
                              "</body> " +
                              "</html>"
                };
            }

            var statusText = "";
            var descriptionText = "";
            
            if (document == null && status == DocumentStatus.Invalid)
            {
                statusText = "Document is INVALID.";
                descriptionText = "Document does not exist.";
            } else if (document != null && status == DocumentStatus.Expired)
            {
                statusText = "Document has EXPIRED.";
                descriptionText = "Document expired at " + DateHelper.ConvertToLocalTimeZone(document.DateOfExpiration);
            } else if (document != null && status == DocumentStatus.Invalid)
            {
                statusText = "Document is INVALID.";
                descriptionText = "Document hash does not match.";
            }

            return new ContentResult
            {
                ContentType = "text/html",
                StatusCode = (int) HttpStatusCode.BadRequest,
                Content = "<html> " +
                          "<head> <meta charset=\"UTF-8\"> </head> " +
                          "<body> " +
                          "<div " +
                          "style=\"top: 50%; left: 50%; transform: translate(-50% , -50%); " +
                          "position: absolute; background: lightcoral; font-size: 30px; font-family: Arial;\">" +
                          "<h1 style=\"margin: 50px;\">" + statusText + "</h1> " +
                          "<h5 style=\"margin: 50px; color: gray;\">" + descriptionText + "</h5>" +
                          "</h3>" +
                          "</div>" +
                          "</body> " +
                          "</html>"
            };
        }
    }
}
