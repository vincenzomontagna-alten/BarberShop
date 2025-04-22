using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLayer.Models
{

        public class Product
        {
            public int Id { get; set; }
            public string Nome { get; set; }
            
            public int? Quantita { get; set; }

            public int? QuantitaMinimaRichiesta { get; set; }

            public ICollection<ProductPrenotation> ProductPrenotationCouples { get; set; } = new List<ProductPrenotation>();

        public Product() { }
        public Product(string name, int quantity, int minimumQuantityRequired)
        {
            Nome = name;
            Quantita = quantity;
            QuantitaMinimaRichiesta = minimumQuantityRequired; 
        }
    }
}
