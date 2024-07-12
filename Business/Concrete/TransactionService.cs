using System;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete
{
    public class TransactionService : ITransactionService
    {
        private readonly IStockRepository _stockRepository;
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IPortfolioItemRepository _portfolioItemRepository;
       

        public TransactionService(IStockRepository stockRepository,
                                   IPortfolioRepository portfolioRepository,
                                   IPortfolioItemRepository portfolioItemRepository
                                 )
        {
            _stockRepository = stockRepository;
            _portfolioRepository = portfolioRepository;
            _portfolioItemRepository = portfolioItemRepository;
        }

        public async Task<TransactionResponse> BuyStockAsync(BuyTransactionRequest request)
        {
            await using var con = new Context();
            await using var transaction = await con.Database.BeginTransactionAsync();
            try
            {
                var stock = await _stockRepository.GetBySymbolAsync(request.Symbol);
                if (stock == null)
                    return new TransactionResponse { Success = false, Message = "Stock not found." };

                var portfolio = await _portfolioRepository.GetByUserIdAsync(request.UserId);
                if (portfolio == null)
                    return new TransactionResponse { Success = false, Message = "Portfolio not found." };

                decimal totalCost = request.Quantity * stock.Price;

                if (portfolio.userBalance < totalCost)
                    return new TransactionResponse { Success = false, Message = "Insufficient balance." };

                var portfolioItem = portfolio.Items.FirstOrDefault(item => item.StockId == stock.Id);
                if (portfolioItem == null)
                {
                    portfolioItem = new PortfolioItem
                    {
                        StockId = stock.Id,
                        Quantity = request.Quantity,
                        PortfolioId = portfolio.Id
                    };
                    await _portfolioItemRepository.AddAsync(portfolioItem);
                }
                else
                {
                    portfolioItem.Quantity += request.Quantity;
                    await _portfolioItemRepository.UpdateAsync(portfolioItem);
                }

                portfolio.userBalance -= totalCost;
                await _portfolioRepository.UpdateAsync(portfolio);

                await transaction.CommitAsync();

                return new TransactionResponse { Success = true, Message = "Stock purchased successfully." };
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return new TransactionResponse { Success = false, Message = "An error occurred during the purchase." };
            }
        }

        public async Task<TransactionResponse> SellStockAsync(SellTransactionRequest request)
        {
            await using var con = new Context();
            await using var transaction = await con.Database.BeginTransactionAsync();
            try
            {
                var portfolio = await _portfolioRepository.GetByUserIdAsync(request.UserId);
                if (portfolio == null)
                    return new TransactionResponse { Success = false, Message = "Portfolio not found." };

                var stock = await _stockRepository.GetBySymbolAsync(request.Symbol);
                if (stock == null)
                    return new TransactionResponse { Success = false, Message = "Stock not found." };

                var portfolioItem = portfolio.Items.FirstOrDefault(item => item.StockId == stock.Id);
                if (portfolioItem == null)
                    return new TransactionResponse { Success = false, Message = "Stock not in portfolio." };

                if (portfolioItem.Quantity < request.Quantity)
                    return new TransactionResponse { Success = false, Message = "Insufficient stocks to sell." };

                portfolioItem.Quantity -= request.Quantity;

                if (portfolioItem.Quantity == 0)
                {
                    portfolio.Items.Remove(portfolioItem);
                    await _portfolioItemRepository.DeleteAsync(portfolioItem);
                }
                else
                {
                    await _portfolioItemRepository.UpdateAsync(portfolioItem);
                }

                portfolio.userBalance += request.Quantity * stock.Price;
                await _portfolioRepository.UpdateAsync(portfolio);

                await transaction.CommitAsync();

                return new TransactionResponse { Success = true, Message = "Stock sold successfully." };
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return new TransactionResponse { Success = false, Message = "An error occurred during the sale." };
            }
        }

        public async Task<Portfolio> GetPortfolioAsync(string userId)
        {
            var portfolio = await _portfolioRepository.GetByUserIdAsync(userId);
            if (portfolio == null)
                return null;

            return portfolio;
        }
    }
}
