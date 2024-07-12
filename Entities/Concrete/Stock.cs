using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Stock
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(3)]
        public string Symbol { get; set; }

        [Required]
        public decimal Price { get; set; }

    }
}
