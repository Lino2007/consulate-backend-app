using System;
using System.Collections.Generic;
using System.Text;
using NSI.DataContracts.Models;

namespace NSI.DataContracts.Dto
{
    public class RequestItemDto
    {
        public RequestItemDto(SimplifiedRequestDto request, List<DocumentDto> document, List<AttachmentDto> attachment)
        {
            this.Request = request;
            this.Document = document;
            this.Attachment = attachment;
        } 

        public SimplifiedRequestDto Request { get; set; }
        public List<DocumentDto> Document { get; set; }
        public List<AttachmentDto> Attachment { get; set; }

    }
}
