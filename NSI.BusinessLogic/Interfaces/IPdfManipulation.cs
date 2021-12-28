using NSI.DataContracts.Models;

namespace NSI.BusinessLogic.Interfaces
{
    public interface IPdfManipulation
    {
        byte[] CreatePassportPdf(Document document, User user, string imageUrl);

        byte[] CreateVisaPdf(Document document, User user, string imageUrl);
    }
}
