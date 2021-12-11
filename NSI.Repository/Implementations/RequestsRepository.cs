﻿using Microsoft.EntityFrameworkCore;
using NSI.DataContracts.Models;
using NSI.DataContracts.Request;
using NSI.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSI.Repository.Implementations
{
    public class RequestsRepository: IRequestsRepository
    {
        private readonly DataContext _context;

        public RequestsRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Request> GetRequestAsync(string id)
        {    
            return await _context.Request.FirstOrDefaultAsync(rq => rq.Id.ToString().Equals(id));
        }

        public async Task<IList<Request>> GetRequestsAsync()
        {
            return await _context.Request.ToListAsync();
        }
        
        public async Task<IList<Request>> GetEmployeeRequestsAsync(string employeeId)
        {
            return await _context.Request.OrderByDescending(d => d.DateCreated).
                   Where(req => req.EmployeeId.ToString().Equals(employeeId)).ToListAsync();
        }

        public async Task<Request> UpdateRequestAsync(ReqItemRequest item)
        {
            var request = await GetRequestAsync(item.id);
            if (request != null)
            {
                if (request.State == item.RequestState)
                    return request;

                request.State = item.RequestState;
                await _context.SaveChangesAsync();
                return request;
            }
            return null;
        }

        public Request SaveRequest(Request request)
        {
            var savedRequest = _context.Request.Add(request).Entity;
            _context.SaveChanges();
            return savedRequest;
        }
    }
}
