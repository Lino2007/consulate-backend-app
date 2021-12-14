using NSI.Common.Collation;
using NSI.Common.Collation.Interfaces;
using NSI.Common.DataContracts.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.DataContracts.Response
{
    public class ReqItemResponse : BaseResponse<IList<NSI.DataContracts.Models.Request>>, IPageable
    {
        public Paging Paging { get; set; }

        public IList<NSI.DataContracts.Models.Request> Requests { get; set; }
    }
}
