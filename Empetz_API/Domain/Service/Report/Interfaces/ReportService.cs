using AutoMapper;
using Domain.Models;
using Domain.Service.Category.Interfaces;
using Domain.Service.Report.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Report.Interfaces
{
    public class ReportService : IReportService
    {
        IReportRepository _repository;
        IMapper _mapper;
        public ReportService(IReportRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<List<ReasonDTO>> GetAllReasons()
        {
            return await _repository.GetAllReasonsAsync();
        }
        public async Task AddReasonAsync(ReasonDTO reasonDTO)
        {
            // Map ReasonDTO to Reason entity if necessary
            var reason = new Reason
            {
                Id = reasonDTO.Id,
                Discription = reasonDTO.Discription
            };

            await _repository.AddReasonAsync(reason);
        }
        public async Task<bool> DeleteReasonAsync(Guid reasonId)
        {
            
            return await _repository.DeleteReasonAsync(reasonId);
        }
        public async Task<IEnumerable<ReportedDTO>> GetAllReportsAsync()
        {
            return await _repository.GetAllReportsAsync();
        }

        public async Task ReportPostAsync(ReportedDTO reportDTO)
        {
            await _repository.ReportPostAsync(reportDTO);
        }
    }
}