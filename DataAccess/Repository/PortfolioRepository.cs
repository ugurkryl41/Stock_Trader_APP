using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class PortfolioRepository : GenericRepository<Portfolio>, IPortfolioRepository
    {
        public async Task<Portfolio> GetByUserIdAsync(string userId)
        {
            await using var con = new Context();

            return await con.Portfolios
                .Include(p=>p.Items)
                    .ThenInclude(pi => pi.Stock)
                    .FirstOrDefaultAsync(p => p.UserId == userId);
        }
    }
}
