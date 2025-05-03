using AutoMapper;
using DAL.Models;
using Domain.Models;
using Domain.Service.Category.DTOs;
using Domain.Service.Report.DTOs;
using Domain.Service.Report.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Report
{
    public class ReportRepository : IReportRepository
    {
        protected readonly EmpetzContext _context;
        IMapper _mapper;
        public ReportRepository(EmpetzContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<ReasonDTO>> GetAllReasonsAsync()
        {
            var Reasons = await _context.Reasons.ToListAsync();
            return _mapper.Map<List<ReasonDTO>>(Reasons);
        }
        public async Task AddReasonAsync(Reason reason)
        {reason.Id = Guid.NewGuid();
            // Add the reason entity to the context
            _context.Reasons.Add(reason);

            try
            {
                // Save changes to the database
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Handle exceptions, log errors, etc.
                throw new Exception("Error occurred while adding reason to the database", ex);
            }
        }

        public async Task<bool> DeleteReasonAsync(Guid reasonId)
        {
            var reason = await _context.Reasons.FindAsync(reasonId);
            if (reason != null)
            {
                _context.Reasons.Remove(reason);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<IEnumerable<ReportedDTO>> GetAllReportsAsync()
        {
            return await _context.ReportedPosts
                  .Select(r => new ReportedDTO
                  {
                      Id = r.Id,
                      Pet = r.Pet,
                      User = r.User,
                      Reason = r.Reason,
                     // Assuming Reason is a navigation property
                  })
                  .ToListAsync();
        }

        public async Task ReportPostAsync(ReportedDTO reportDTO)
        {
            var reportedPost = new ReportedPost
            {
                Id = Guid.NewGuid(),
                Pet = reportDTO.Pet,
                User = reportDTO.User,
                Reason = reportDTO.Reason
            };

            await _context.ReportedPosts.AddAsync(reportedPost);
            await _context.SaveChangesAsync();
        }
    }
}
