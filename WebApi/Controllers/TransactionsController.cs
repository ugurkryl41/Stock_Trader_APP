using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost("buy")]
        public async Task<IActionResult> BuyStock(BuyTransactionRequest request)
        {
            var response = await _transactionService.BuyStockAsync(request);
            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpPost("sell")]
        public async Task<IActionResult> SellStock(SellTransactionRequest request)
        {
            var response = await _transactionService.SellStockAsync(request);
            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpGet("portfolio")]
        public async Task<IActionResult> GetPortfolio(string userId)
        {
            var portfolio = await _transactionService.GetPortfolioAsync(userId);
            if (portfolio == null)
                return NotFound("Portfolio not found.");

            return Ok(portfolio);
        }
    }
}
