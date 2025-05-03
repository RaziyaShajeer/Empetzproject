using Domain.Models;
using Domain.Service.Report.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Report.Interfaces
{
    public interface IReportRepository
    {
        Task AddReasonAsync(Reason reason);
        Task<bool> DeleteReasonAsync(Guid reasonId);
        Task<List<ReasonDTO>> GetAllReasonsAsync();
        Task<IEnumerable<ReportedDTO>> GetAllReportsAsync();
        Task ReportPostAsync(ReportedDTO reportDTO);
    }
}
