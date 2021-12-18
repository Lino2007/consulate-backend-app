using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NSI.BusinessLogic.Interfaces
{
    public interface IFilesManipulation
    {
        Task<string> UploadFile(IFormFile file, string fileName);
        Task<string> DownloadFile(string fileName);
        Task ListFiles();
    }
}
