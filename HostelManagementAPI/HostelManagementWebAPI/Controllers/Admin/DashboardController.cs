using DTOs.Dashboard;
using HostelManagementWebAPI.MessageStatusResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace HostelManagementWebAPI.Controllers.Admin
{
    [ApiController]
    public class DashboardController : BaseApiController
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [Authorize(policy: "Admin")]
        [HttpGet("admin/dashboard")]
        public async Task<ActionResult<Dashboard>> GetSatitic()
        {

            try
            {
                var dashboard = await _dashboardService.GetStatiticDashboar();

                return Ok(dashboard);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseStatus(500, ex.Message));
            }
        }

        [Authorize(policy: "Admin")]
        [HttpGet("admin/dashboard/typepackage")]
        public async Task<ActionResult<IEnumerable<AccountEachMemberShipDtos>>> GetAmountAccountEachMemberShip()
        {
            var memberships = await _dashboardService.GetAmountAccountEachMemberShip();
            return Ok(memberships);
        }

        [Authorize(policy: "Admin")]
        [HttpGet("admin/dashboard/typemonth")]
        public async Task<ActionResult<IEnumerable<TypeMonthDtos>>> GetAmountProfitEachMonth(int year)
        {
            var profits = await _dashboardService.GetAmountProfitEachMonth(year);
            return Ok(profits);
        }

        [Authorize(policy: "Admin")]
        [HttpGet("admin/dashboard/accountmonth")]
        public async Task<ActionResult<IEnumerable<TypeMonthDtos>>> GetAmountAccountEachMonth(int year)
        {
            var profits = await _dashboardService.GetAmountAccountEachMonth(year);
            return Ok(profits);
        }
    }
}
