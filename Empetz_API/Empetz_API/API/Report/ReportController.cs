using AutoMapper;
using Domain.Service.Category.DTOs;
using Domain.Service.Category;
using Domain.Service.Report.Interfaces;
using Empetz.Controllers;
using Empetz_API.API.Category.RequestObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Service.Report.DTOs;
using Empetz_API.API.Report.RequestObject;
using Microsoft.AspNetCore.Authorization;

namespace Empetz_API.API.Report
{

    [ApiController]
    [Authorize(Roles = "PublicUser")]
    public class ReportController : BaseApiController<ReportController>
    {
        public IReportService reportService;

        IMapper _mapper;
        public ReportController(IReportService _reportService, IMapper mapper)
        {
            reportService = _reportService;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("reasons")]
        public async Task<IActionResult> GetAllReasons()
        {
            try
            {
                List<ReasonDTO> Reason = await reportService.GetAllReasons();
                return Ok(_mapper.Map<List<ReasonRequest>>(Reason));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        [HttpPost("report")]
        public async Task<IActionResult> ReportPost(RepoprtPostRequest request)
        {
            try
            {
                var reportDTO = new ReportedDTO
                {

                    Pet = request.Pet,
                    User = request.User,
                    Reason = request.Reason
                };

                await reportService.ReportPostAsync(reportDTO);
                return Ok("Pet post reported successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
        [HttpGet("reported-post")]
        public async Task<IActionResult> GetAllReports()
        {
            try
            {
                var reports = await reportService.GetAllReportsAsync();
                return Ok(reports);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpPost("reason")]
        public async Task<IActionResult> AddReason([FromBody] ReasonDTO reasonDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await reportService.AddReasonAsync(reasonDTO);
                return Ok("Reason added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete("reason/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await reportService.DeleteReasonAsync(id);
            if (success)
            {
                return Ok("Reason deleted successfully.");
            }
            return NotFound();
        }
    }

}

