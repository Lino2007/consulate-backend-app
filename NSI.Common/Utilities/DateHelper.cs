using System;

namespace NSI.Common.Utilities
{
    public static class DateHelper
    {
        public static DateTime ConvertToLocalTimeZone(DateTime dateTime)
        {
            TimeZoneInfo localTimeZone;
            
            try
            {
                localTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
            }
            catch (TimeZoneNotFoundException)
            {
                localTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Sarajevo");
            }

            return TimeZoneInfo.ConvertTimeFromUtc(dateTime, localTimeZone);
        }
    }
}
