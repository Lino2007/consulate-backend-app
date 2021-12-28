using DinkToPdf;
using DinkToPdf.Contracts;
using NSI.BusinessLogic.Interfaces;
using NSI.BusinessLogic.Utilities;
using NSI.DataContracts.Models;

namespace NSI.BusinessLogic.Implementations
{
    public class PdfManipulation : IPdfManipulation
    {
        private readonly IConverter _converter;

        public PdfManipulation(IConverter converter)
        {
            _converter = converter;
        }

        public byte[] CreatePassportPdf(Document document, User user, string imageUrl)
        {
            return CreatePdf(document, user, imageUrl);
        }

        public byte[] CreateVisaPdf(Document document, User user, string imageUrl)
        {
            return CreatePdf(document, user, imageUrl);
        }

        private byte[] CreatePdf(Document document, User user, string imageUrl)
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings {Top = 10},
                DocumentTitle = document.Type.Name + "-" + document.Id,
                //Out = @"D:\" + document.Type.Name + "-" + document.Id + ".pdf"
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = document.Type.Name.Equals("Passport")
                    ? TemplateGenerator.GetPassportHtmlString(document, user, imageUrl)
                    : TemplateGenerator.GetVisaHtmlString(document, user, imageUrl),
                WebSettings = { DefaultEncoding = "utf-8" },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Consulate General Of Bosnia And Herzegovina" }
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            return _converter.Convert(pdf);
        }
    }
}
