using Application.DTOs;
using Application.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace sistemas.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly ISalesService _salesService;

        public SalesController(ISalesService salesService)
        {
            _salesService = salesService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSale(SaleRequestDto dto)
        {
            try
            {
                var result = await _salesService.CreateSaleAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] DateTime? from, [FromQuery] DateTime? to)
        {
            var sales = await _salesService.GetAllSalesAsync(from, to);
            return Ok(sales);
        }
    }
}
