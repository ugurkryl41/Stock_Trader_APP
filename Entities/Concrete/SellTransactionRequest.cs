using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete
{
    public class SellTransactionRequest
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Symbol { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero.")]
        public int Quantity { get; set; }
    }
}
