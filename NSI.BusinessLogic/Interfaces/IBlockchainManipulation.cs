using System.IO;
using System.Threading.Tasks;

namespace NSI.BusinessLogic.Interfaces
{
    public interface IBlockchainManipulation
    {
        Task<string> UploadDocument(string documentId, Stream stream);
        Task<bool> ValidateDocument(string blockchainId, string documentHash);
    }
}
