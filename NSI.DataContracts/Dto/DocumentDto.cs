using NSI.DataContracts.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace NSI.DataContracts.Dto
{
    public class DocumentDto
    {
        public DocumentDto(Document document)
        {
            Id = document.Id.ToString();
            DateCreated = document.DateCreated;
            DateOfExpiration = document.DateOfExpiration;
            Url = document.Url;
            Active = document.Active;
            Title = document.Title;
            DocumentType = document.Type?.Name;
        }

        string Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateOfExpiration { get; set; }
        public string Url { get; set; }
        public bool Active { get; set; }
        public string Title { get; set; }
        public string DocumentType { get; set; }

    }
}
