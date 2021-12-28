using System;
using NSI.DataContracts.Models;

namespace NSI.BusinessLogic.Utilities
{
    public static class TemplateGenerator
    {
        public static string GetPassportHtmlString(Document document, User user, string imageUrl)
        {
            return GetHtmlString(document, user, imageUrl);
        }

        public static string GetVisaHtmlString(Document document, User user, string imageUrl)
        {
            return GetHtmlString(document, user, imageUrl);
        }

        private static string GetHtmlString(Document document, User user, string imageUrl)
        {
            return @"
                     <div>
                        <h1>" + document.Type.Name + @"</h1>
                        <p><strong>This document was issued electronically and is therefore valid without signature</strong>.</p>
                     </div>
                     <table>
                        <tbody>
                           <tr>
                              <th style=""text-align: left; width: 115px;"">Document ID:</th>
                              <td>" + document.Id + @"</td>
                           </tr>
                           <tr>
                              <th style=""text-align: left; width: 115px;"">Date created:</th>
                              <td>" + TimeZoneInfo.ConvertTimeFromUtc(document.DateCreated, TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time")).ToString("dd.MM.yyyy HH:mm") + @"</td>
                           </tr>
                        </tbody>
                     </table>
                     <h3>User information</h3>
                     <table>
                        <tbody>
                           <tr>
                              <th style=""text-align: left; width: 115px;"">First Name:</th>
                              <td>" + user.FirstName + @"</td>
                           </tr>
                           <tr>
                              <th style=""width: 115px; text-align: left;"">Last Name:</th>
                              <td>" + user.LastName + @"</td>
                           </tr>
                           <tr>
                              <th style=""width: 115px; text-align: left;"">Email:</th>
                              <td>" + user.Email + @"</td>
                           </tr>
                           <tr>
                              <th style=""width: 115px; text-align: left;"">Username:</th>
                              <td>" + user.Username + @"</td>
                           </tr>
                           <tr>
                              <th style=""width: 115px; text-align: left;"">Place of Birth:</th>
                              <td>" + user.PlaceOfBirth + @"</td>
                           </tr>
                           <tr>
                              <th style=""width: 115px; text-align: left;"">Date of Birth:</th>
                              <td>" + TimeZoneInfo.ConvertTimeFromUtc(user.DateOfBirth, TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time")).ToString("dd.MM.yyyy") + @"</td>
                           </tr>
                           <tr>
                              <th style=""width: 115px; text-align: left;"">Country:</th>
                              <td>" + user.Country + @"</td>
                           </tr>
                        </tbody>
                     </table>

                     <p><strong>Scan following QR code to check if document is valid:</strong>.</p>
                     <img src=""" + imageUrl + @""" alt=""QR code"" width=""500"" height=""600"">
                    
                     <p style=""font-size: 12px;"">" +
                   (document.Type.Name.Equals("Passport")
                       ? "The Consulate General of Bosnia and Herzegovina in Frankfurt issues passports to citizens of Bosnia and Herzegovina who stay abroad for more than three months and to those who stay less than three months if the passport is destroyed, damaged, stolen or lost. The passport of Bosnia and Herzegovina is a document on the basis of which the identity and citizenship of the holder is determined. It is issued with a validity period of 10 years, for children under 3 years of age with a validity period of up to 3 years, for BiH citizens from 3 to 18 years of age with a validity period of 5 years."
                       : "In addition to a biometric passport, BiH citizens need to have money in the amount of 35-70 euros for each day of stay in EU countries to travel to EU member states and Schengen countries. Border services may require proof of possession of money to stay in a particular country or information on the address where you will be staying.")
                   + "</p>";
        }
    }
}
