using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ITransactionService
    {
        Task<TransactionResponse> BuyStockAsync(BuyTransactionRequest request);
        Task<TransactionResponse> SellStockAsync(SellTransactionRequest request);
        Task<Portfolio> GetPortfolioAsync(string userId);
    }

}
