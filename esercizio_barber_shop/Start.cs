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
        private readonly ProductAdoService _productAdoService;
        public Start(Context context, ProductService productService,ProductAdoService productAdoService) 
        {
            _context = context;
            _productService = productService;
            _productAdoService = productAdoService;

        }


        public void Execute()
        {

            while (true)
            {
                Console.WriteLine("Dimmi che cosa vuoi fare:");
                Console.WriteLine("1 - Gestisci i prodotti del magazzino");
                Console.WriteLine("2 - Gestisci i prodotti del magazzino con ADO.NET");
                

                string choose = Console.ReadLine();

                switch (choose)
                {
                    case "1":
                        _productService.ManageProducts();
                        break;
                    case "2":
                        _productAdoService.ManageProducts();
                        break;
                    default:
                        Console.WriteLine("Scelta non valida");
                        break;


                }

            }
        }
    }
}
