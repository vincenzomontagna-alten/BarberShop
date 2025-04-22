using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLayer.Models
{
    public class ProductPrenotation
    {
        public int ProductId { get; set; }
        public int PrenotationId { get; set; }

        public Product Product { get; set; }
        public Prenotation Prenotation { get; set; }
    }
}
