using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class StockRepository : GenericRepository<Stock>, IStockRepository
    {

        public async Task<Stock> GetBySymbolAsync(string symbol)
        {
            await using var con = new Context();
            return await con.Stocks.FirstOrDefaultAsync(s => s.Symbol == symbol);
        }
    }
}
