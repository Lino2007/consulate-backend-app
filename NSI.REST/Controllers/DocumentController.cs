using System;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSI.BusinessLogic.Interfaces;
using NSI.REST.Filters;

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
        /// Get HTML validation page for document by id.
        /// </summary>
        [Authorize]
        [PermissionCheck("document:view")]
        [HttpGet("{id}")]
        public ContentResult GetDocumentIfNotExpired([FromRoute] Guid id)
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

            var document = _documentsManipulation.GetDocumentIfNotExpired(id);
            if (document != null)
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
                              "<h5 style=\"margin: 50px; color: gray;\">Expiration Date: " + document.DateOfExpiration +
                              "</h5>" +
                              "</div> " +
                              "</body> " +
                              "</html>"
                };
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
                          "<h1 style=\"margin: 50px;\">Document is INVALID.</h1> " +
                          "<h5 style=\"margin: 50px; color: gray;\">Document does not exist or it has expired.</h5>" +
                          "</h3>" +
                          "</div>" +
                          "</body> " +
                          "</html>"
            };
        }
    }
}
