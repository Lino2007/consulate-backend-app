using NSI.Common.DataContracts.Base;
using System;
using System.Collections.Generic;
using System.Text;
using NSI.DataContracts.Models;
using NSI.Common.Collation.Interfaces;
using NSI.Common.Collation;
using NSI.DataContracts.Dto;

namespace NSI.DataContracts.Response
{
    public class ReqItemListResponse : BaseResponse<IList<RequestItemDto>>, IPageable
    {
        public Paging Paging { get; set; }

        public IList<RequestItemDto> Requests { get; set;}
    }
}
