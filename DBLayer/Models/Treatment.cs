using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLayer.Models
{
    public class Treatment
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal? Cost { get; set; }
        public int? Duration { get; set; }

        public ICollection<Prenotation> Prenotations { get; set; } = new List<Prenotation>(); 
    }
}
