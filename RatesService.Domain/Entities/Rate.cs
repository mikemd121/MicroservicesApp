using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatesService
{
   public class Rate
    {
        [Key]
        public string Symbol { get; set; }
        public decimal Price { get; set; }
    }
}
