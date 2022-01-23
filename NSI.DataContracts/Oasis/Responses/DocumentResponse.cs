using System;
using System.Diagnostics.CodeAnalysis;
using NSI.DataContracts.Oasis.Models;

namespace NSI.DataContracts.Oasis.Responses
{
    [ExcludeFromCodeCoverage]
    public class DocumentResponse
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Creator { get; set; }
        public string Owner { get; set; }
        public int Size { get; set; }
        public Document Details { get; set; }
        public string OriginatingJob { get; set; }
    }
}
