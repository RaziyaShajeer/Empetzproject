using Domain.Service.Report.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Report.Interfaces
{
    public interface IReportService
    {
        Task AddReasonAsync(ReasonDTO reasonDTO);
        Task<bool> DeleteReasonAsync(Guid id);
        Task<List<ReasonDTO>> GetAllReasons();
        Task<IEnumerable<ReportedDTO>> GetAllReportsAsync();
        Task ReportPostAsync(ReportedDTO reportDTO);
    }
}
