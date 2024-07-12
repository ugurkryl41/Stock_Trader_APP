using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class PortfolioItemRepository : GenericRepository<PortfolioItem>, IPortfolioItemRepository
    {
        public async Task<PortfolioItem> GetByPortfolioAndStockAsync(int portfolioId, int stockId)
        {
            await using var con = new Context();
            return await con.PortfolioItems.FirstOrDefaultAsync(pi => pi.PortfolioId == portfolioId && pi.StockId == stockId);
        }
    }
}
