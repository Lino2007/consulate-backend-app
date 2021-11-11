using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Common.DataContracts.Enumerations
{
    /// <summary>
    /// Used to indicate whether request was successful or not 
    /// </summary>
    public enum ResponseStatus
    {
        /// <summary>
        /// Request could not be processed, an error has occurred
        /// </summary>
        Failed = 0,
        /// <summary>
        /// Request was successfully processed
        /// </summary>
        Succeeded = 1
    }
}
