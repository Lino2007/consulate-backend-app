using NSI.Common.DataContracts.Base;
using System;
using System.Collections.Generic;
using System.Text;
using NSI.DataContracts.Models;
using NSI.Common.Collation.Interfaces;
using NSI.Common.Collation;

namespace NSI.DataContracts.Response
{
    public class ReqResponse : BaseResponse<IList<NSI.DataContracts.Models.Request>>, IPageable
    {
        public Paging Paging { get; set; }

        public IList<NSI.DataContracts.Models.Request> Requests { get; set;}
    }
}
