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

    }
}
