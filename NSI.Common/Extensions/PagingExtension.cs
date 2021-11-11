using NSI.Common.Collation;
using NSI.Common.Exceptions;
using NSI.Common.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Common.Extensions
{
    public static class PagingExtension
    {
        /// <summary>
        /// Performs sanity validation of paging criteria. Does not validate if paging object is null.
        /// </summary>
        /// <param name="paging"><see cref="Paging"/></param>
        public static void ValidatePagingCriteria(this Paging paging)
        {
            if (paging != null)
            {
                if (paging.Page <= 0)
                {
                    throw new NsiArgumentException(ExceptionMessages.InvalidPage);
                }

                if (paging.RecordsPerPage <= 0)
                {
                    throw new NsiArgumentException(ExceptionMessages.InvalidRecordsPerPage);
                }
            }
        }
    }
}
