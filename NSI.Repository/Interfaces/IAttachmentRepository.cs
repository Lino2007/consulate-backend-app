using NSI.DataContracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NSI.Repository.Interfaces
{
    public interface IAttachmentRepository
    {
        public Task<IList<Attachment>> GetAttachmentsByRequests(List<Request> req);
        Attachment SaveAttachment(Attachment attachment);
        Attachment UpdateAttachment(Attachment attachment);
    }
}
