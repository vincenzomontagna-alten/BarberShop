using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLayer.Models
{
    public class Prenotation
    {
        public int Id { get; set; } 
        public DateTime? DayAndHour { get; set; }

        public int? TreatmentId { get; set; }
        public int? CustomerId { get; set; }

        public Treatment Treatment { get; set; }

        public Customer Customer { get; set; }

        public ICollection<ProductPrenotation> ProductPrenotationCouples { get; set; } = new List<ProductPrenotation>();

    }
}
