using NSI.Common.Collation;
using NSI.Common.Collation.Interfaces;
using NSI.Common.DataContracts.Base;
using NSI.DataContracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.DataContracts.Response
{
    public class UserResponse : BaseResponse<IList<User>>, IPageable
    {
        public IList<User> Permissions { get; set; }
        public Paging Paging { get; set; }
    }
}
