using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Portfolio
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public decimal userBalance { get; set; }
        public ICollection<PortfolioItem> Items { get; set; } = new List<PortfolioItem>();
    }
}
