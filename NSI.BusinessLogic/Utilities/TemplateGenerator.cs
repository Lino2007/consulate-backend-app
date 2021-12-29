using System;
using NSI.DataContracts.Models;

namespace NSI.BusinessLogic.Utilities
{
    public static class TemplateGenerator
    {
        public static string GetPassportHtmlString(Document document, User user,  string qrImageBase64, string qrImageUrl)
        {
            return GetHtmlString(document, user, qrImageBase64, qrImageUrl);
        }

        public static string GetVisaHtmlString(Document document, User user,  string qrImageBase64, string qrImageUrl)
        {
            return GetHtmlString(document, user, qrImageBase64, qrImageUrl);
        }

        private static string GetHtmlString(Document document, User user,  string qrImageBase64, string qrImageUrl)
        {
            return @"
                     <div>
                        <h1 style=""font-size: 120px;"">" + document.Type.Name + @"</h1>
                        <p style=""font-size: 60px; color: gray;"">
                           <strong>
                              This document was issued electronically and is therefore valid without signature.
                           </strong>
                        </p>
                     </div>
                     <table>
                        <tbody>
                           <tr>
                              <th style=""text-align: left; width: 500px; font-size: 60px;"">Document ID:</th>
                              <td style=""font-size: 60px;"">" + document.Id + @"</td>
                           </tr>
                           <tr>
                              <th style=""text-align: left; width: 500px; font-size: 60px;"">Date created:</th>
                              <td style=""font-size: 60px;"">" + TimeZoneInfo.ConvertTimeFromUtc(document.DateCreated, TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time")).ToString("dd.MM.yyyy HH:mm") + @"</td>
                           </tr>
                        </tbody>
                     </table>
                     <h3 style=""font-size: 70px;"">User information</h3>
                     <table>
                        <tbody>
                           <tr>
                              <th style=""width: 500px; text-align: left; font-size: 60px;"">First Name:</th>
                              <td style=""font-size: 60px;"">" + user.FirstName + @"</td>
                           </tr>
                           <tr>
                              <th style=""width: 500px; text-align: left; font-size: 60px;"">Last Name:</th>
                              <td style=""font-size: 60px;"">" + user.LastName + @"</td>
                           </tr>
                           <tr>
                              <th style=""width: 500px; text-align: left; font-size: 60px;"">Email:</th>
                              <td style=""font-size: 60px;"">" + user.Email + @"</td>
                           </tr>
                           <tr>
                              <th style=""width: 500px; text-align: left; font-size: 60px;"">Username:</th>
                              <td style=""font-size: 60px;"">" + user.Username + @"</td>
                           </tr>
                           <tr>
                              <th style=""width: 500px; text-align: left; font-size: 60px;"">Place of Birth:</th>
                              <td style=""font-size: 60px;"">" + user.PlaceOfBirth + @"</td>
                           </tr>
                           <tr>
                              <th style=""width: 500px; text-align: left; font-size: 60px;"">Date of Birth:</th>
                              <td style=""font-size: 60px;"">" + TimeZoneInfo.ConvertTimeFromUtc(user.DateOfBirth, TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time")).ToString("dd.MM.yyyy") + @"</td>
                           </tr>
                           <tr>
                              <th style=""width: 500px; text-align: left; font-size: 60px;"">Country:</th>
                              <td style=""font-size: 60px;"">" + user.Country + @"</td>
                           </tr>
                        </tbody>
                     </table>

                     <p style=""font-size: 70px;"">
                        <strong>
                        Scan following <a href=""" + qrImageUrl + @""">QR code</a> to check if document is valid:
                        </strong>
                     </p>
                     <img src=""" + qrImageBase64 + @""" alt=""QR code"" style=""width:750px;height:750px;"">
                   
                     <p style=""font-size: 60px;"">" +
                   (document.Type.Name.Equals("Passport")
                       ? "The Consulate General of Bosnia and Herzegovina in Frankfurt issues passports to citizens of Bosnia and Herzegovina who stay abroad for more than three months and to those who stay less than three months if the passport is destroyed, damaged, stolen or lost. The passport of Bosnia and Herzegovina is a document on the basis of which the identity and citizenship of the holder is determined. It is issued with a validity period of 10 years, for children under 3 years of age with a validity period of up to 3 years, for BiH citizens from 3 to 18 years of age with a validity period of 5 years."
                       : "In addition to a biometric passport, BiH citizens need to have money in the amount of 35-70 euros for each day of stay in EU countries to travel to EU member states and Schengen countries. Border services may require proof of possession of money to stay in a particular country or information on the address where you will be staying.")
                   + "</p>";
        }
    }
}
