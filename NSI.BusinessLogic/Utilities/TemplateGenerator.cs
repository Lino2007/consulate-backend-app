using System;
using NSI.Common.Utilities;
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
                        <h1>" + document.Type.Name + @"</h1>
                        <p style=""color: gray;"">
                           <strong>
                              This document was issued electronically and is therefore valid without signature.
                           </strong>
                        </p>
                     </div>
                     <table>
                        <tbody>
                           <tr>
                              <th style=""text-align: left; width: 500px;"">Document ID:</th>
                              <td>" + document.Id + @"</td>
                           </tr>
                           <tr>
                              <th style=""text-align: left; width: 500px;"">Date created:</th>
                              <td>" + DateHelper.ConvertToLocalTimeZone(document.DateCreated).ToString("dd.MM.yyyy HH:mm") + @"</td>
                           </tr>
                        </tbody>
                     </table>
                     <h3>User information</h3>
                     <table>
                        <tbody>
                           <tr>
                              <th style=""width: 500px; text-align: left;"">First Name:</th>
                              <td>" + user.FirstName + @"</td>
                           </tr>
                           <tr>
                              <th style=""width: 500px; text-align: left;"">Last Name:</th>
                              <td>" + user.LastName + @"</td>
                           </tr>
                           <tr>
                              <th style=""width: 500px; text-align: left;"">Email:</th>
                              <td>" + user.Email + @"</td>
                           </tr>
                           <tr>
                              <th style=""width: 500px; text-align: left;"">Username:</th>
                              <td>" + user.Username + @"</td>
                           </tr>
                           <tr>
                              <th style=""width: 500px; text-align: left;"">Place of Birth:</th>
                              <td>" + user.PlaceOfBirth + @"</td>
                           </tr>
                           <tr>
                              <th style=""width: 500px; text-align: left;"">Date of Birth:</th>
                              <td>" + DateHelper.ConvertToLocalTimeZone(user.DateOfBirth).ToString("dd.MM.yyyy") + @"</td>
                           </tr>
                           <tr>
                              <th style=""width: 500px; text-align: left;"">Country:</th>
                              <td>" + user.Country + @"</td>
                           </tr>
                        </tbody>
                     </table>

                     <p>
                        <strong>
                        Scan following <a href=""" + qrImageUrl + @""">QR code</a> to check if document is valid:
                        </strong>
                     </p>
                     <img src=""" + qrImageBase64 + @""" alt=""QR code"" style=""width:350px;height:350px;"">
                   
                     <p style=""font-size: 12px;"">" +
                  (document.Type.Name.Equals("Passport")
                     ? "The Consulate General of Bosnia and Herzegovina in Frankfurt issues passports to citizens of Bosnia and Herzegovina who stay abroad for more than three months and to those who stay less than three months if the passport is destroyed, damaged, stolen or lost. The passport of Bosnia and Herzegovina is a document on the basis of which the identity and citizenship of the holder is determined. It is issued with a validity period of 10 years, for children under 3 years of age with a validity period of up to 3 years, for BiH citizens from 3 to 18 years of age with a validity period of 5 years."
                     : "In addition to a biometric passport, BiH citizens need to have money in the amount of 35-70 euros for each day of stay in EU countries to travel to EU member states and Schengen countries. Border services may require proof of possession of money to stay in a particular country or information on the address where you will be staying.")
                  + "</p>";
        }
    }
}
