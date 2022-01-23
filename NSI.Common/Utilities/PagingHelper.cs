using NSI.Common.Collation;
using NSI.Common.Resources;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace NSI.Common.Utilities
{
    [ExcludeFromCodeCoverage]
    public static class PagingHelper
    {
        #region Paging helper methods

        /// <summary>
        /// Calculate and validate paging details, like number of pages, records per page etc.. 
        /// </summary>
        /// <param name="paging">Paging details</param>
        /// <param name="totalRecords">Total number of records/entities for which paging is implemented.</param>
        /// <param name="skipElements">Number of elements that should be skipped when getting data.</param>
        /// <param name="takeElements">Number of elements that should be takken.</param>
        public static void CalculatePagingDetails(Paging paging, int totalRecords, out int skipElements, out int takeElements)
        {
            if (paging == null)
            {
                throw new ArgumentException(ExceptionMessages.PagingParameterNull);
            }

            // Apply paging
            paging.TotalRecords = totalRecords;

            if (paging.RecordsPerPage <= 0)
            {
                paging.RecordsPerPage = totalRecords > 0 ? totalRecords : 1;
            }

            takeElements = paging.RecordsPerPage;

            if (paging.Page >= paging.Pages)
            {
                paging.Page = paging.Pages;
            }
            else if (paging.Page <= 0)
            {
                paging.Page = 1;
            }

            skipElements = paging.Page > 0 ? (paging.Page - 1) * paging.RecordsPerPage : 0;
        }

        #endregion
    }
}
