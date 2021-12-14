using NSI.Common.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.DataContracts.Dto
{
    // <summary>
    // Simplified request object wrapper. 
    // </summary>
    public class SimplifiedRequestDto
    {
        public SimplifiedRequestDto(Models.Request request, string user, string employee)
        {
            Id = request.Id.ToString();
            User = user;
            Employee = employee;
            DateCreated = request.DateCreated;
            Reason = request.Reason;
            Type = request.Type;
            State = request.State;
        }
        public string Id { get; set; }
        public string User { get; set; }
        public string Employee { get; set; }
        public DateTime DateCreated { get; set; }
        public string Reason { get; set; }
        public RequestType Type { get; set; }
        public RequestState State { get; set; }

    }
}
