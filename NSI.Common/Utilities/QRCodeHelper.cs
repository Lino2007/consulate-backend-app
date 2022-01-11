using System.Drawing;
using QRCoder;

namespace NSI.Common.Utilities
{
    public static class QRCodeHelper
    {
        public static Bitmap GenerateBitmap(string content)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData data = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(data);
            Bitmap qrCodeImage = qrCode.GetGraphic(5);
            return qrCodeImage;
        }
    }
}
