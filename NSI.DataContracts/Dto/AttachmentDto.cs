using NSI.DataContracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.DataContracts.Dto
{
    public class AttachmentDto
    {
        public AttachmentDto(Attachment attachment)
        {
            Id = attachment.Id.ToString();
            Url = attachment.Url;
            DocumentType = attachment.Type?.Name;
        }

        public string Id { get; set; }
        public string Url { get; set; }
        public string DocumentType { get; set;}
    }
}
