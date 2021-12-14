﻿using NSI.DataContracts.Models;
using NSI.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace NSI.Repository.Implementations
{
    public class AttachmentRepository : IAttachmentRepository
    {
        private readonly DataContext _context;

        public AttachmentRepository(DataContext context)
        {
            _context = context;
        }
        
        /// <summary>
        /// The method that returns all attachments for list of requests.
        /// </summary>
        public async Task<IList<Attachment>> getAttachmentsByRequests(List<Request> RequestList)
        {
        
            if (RequestList == null || RequestList.Count == 0)
            {
                return null;
            }

            var idList = RequestList.Select(x => x.Id).ToList();
            var result = await (from att in _context.Attachment
                                 from dt in _context.DocumentType
                                 where idList.Contains(att.RequestId) && att.TypeId == dt.Id
                                 select new { att, dt }).ToListAsync();
                                    
            return result.Select((item) =>
                                          { // attachment klasa ima DocumentType property (Type) koji nakon prvog querija bude null
                                            item.att.Type = item.dt;
                                            return item.att;  }).ToList(); 
        }
    }
}