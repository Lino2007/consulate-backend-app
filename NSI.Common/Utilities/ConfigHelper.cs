using NSI.Common.Exceptions;
using NSI.Common.Resources;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace NSI.Common.Utilities
{
    /// <summary>
    /// Contains methods for retrieving values from configuration files
    /// </summary>
    public static class ConfigHelper
    {
        /// <summary>
        /// Retrieves the value for the provided key from configuration file
        /// </summary>
        /// <param name="key">Key name to search the configuration for</param>
        /// <param name="throwExceptionIfKeyNotFound">Signals whether exception should be thrown if key could not be found</param>
        /// <returns>The value matching the provided configuration key</returns>
        /// <exception cref="NsiConfigurationException">Thrown when provided key could not be found and <paramref name="throwExceptionIfKeyNotFound"/> is set to true/></exception>
        public static string GetValue(string key, bool throwExceptionIfKeyNotFound = true)
        {
            var value = ConfigurationManager.AppSettings[key];

            if (value == null && throwExceptionIfKeyNotFound)
            {
                throw new NsiConfigurationException(string.Format(ExceptionMessages.ProvidedKeyDoesNotExist, key));
            }

            return value;
        }
    }
}
