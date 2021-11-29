using NSI.Common.DataContracts.Base;
using NSI.Common.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.DataContracts.Request
{
    public class NewUserRequest : BaseRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Gender { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string PlaceOfBirth { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Country { get; set; }
    }
}
