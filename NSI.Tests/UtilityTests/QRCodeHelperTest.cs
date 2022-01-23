using System.Drawing;
using NSI.Common.Utilities;
using Xunit;

namespace NSI.Tests.UtilityTests
{
    public class QRCodeHelperTest
    {
        [Fact]
        public void GetBitmap()
        {
            var bitmap = QRCodeHelper.GenerateBitmap(
                "https://localhost:5001/public/Document/3fa85f64-5717-4562-b3fc-2c963f66afa6");
            
            Assert.NotNull(bitmap);
            Assert.IsType<Bitmap>(bitmap);
            Assert.Equal(265, bitmap.Width);
            Assert.Equal(265, bitmap.Height);   
        }
    }
}
