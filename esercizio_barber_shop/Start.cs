using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBLayer.Data;
using DBLayer.Models;




namespace esercizio_barber_shop

{
    public class Start
    {
        private readonly Context _context;
        public Start(Context context) 
        {
            _context = context;
        }
        public void Execute()
        {
            foreach (var p in _context.Products)
            {
                Console.WriteLine(p.Id + " " + p.Nome + " " + p.Quantita + " " + p.QuantitaMinimaRichiesta );            
            }
        }
    }
}
