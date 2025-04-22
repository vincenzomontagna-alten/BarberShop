using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBLayer.Data;
using DBLayer.Models;
using ServiceLayer;




namespace esercizio_barber_shop

{
    public class Start
    {
        private readonly Context _context;
        private readonly ProductService _productService;
        public Start(Context context, ProductService productService) 
        {
            _context = context;
            _productService = productService;

        }


        public void Execute()
        {
            try
            {
                var productPrenotationCouples = _context.ProductPrenotationCouples.ToList();
                Console.WriteLine("Caricamento riuscito.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore: {ex.Message}");
            }
            while (true)
            {
                Console.WriteLine("Dimmi che cosa vuoi fare:");
                Console.WriteLine("1 - Gestisci i prodotti del magazzino");

                string choose = Console.ReadLine();

                switch (choose)
                {
                    case "1":
                        _productService.ManageProducts();
                        break;
                    default:
                        Console.WriteLine("Scelta non valida");
                        break;


                }

            }
        }
    }
}
