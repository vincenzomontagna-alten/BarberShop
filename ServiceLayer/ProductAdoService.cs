using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBLayer.Interfaces;
using DBLayer.Models;
using DBLayer.Repositories;

namespace ServiceLayer
{
    public class ProductAdoService
    {
        private readonly ProductAdoRepository _productAdoRepository;
        private readonly ProductService _productService;

        public ProductAdoService(ProductAdoRepository productAdoRepository, ProductService productService)
        {
            _productAdoRepository = productAdoRepository;
            _productService = productService;
        }

        public void ManageProducts()
        {
            while (true)
            {
                Console.WriteLine("Dimmi che cosa vuoi fare:");
                Console.WriteLine("1 - Mostra la lista dei prodotti");
                Console.WriteLine("2 - Aggiungi un prodotto");
                Console.WriteLine("3 - Elimina un prodotto");
                Console.WriteLine("4 - Modifica un prodotto");
                Console.WriteLine("5 - Fai la lista della spesa");

                string choose = Console.ReadLine();

                switch (choose)
                {
                    case "1":
                        ShowListProducts();
                        break;
                    case "2":
                        AddProduct();
                        break;
                    case "3":
                        DeleteProduct();
                        break;
                    case "4":
                        UpdateProduct();
                        break;
                    default:
                        Console.WriteLine("Scelta non valida");
                        break;
                }
            }
        }
        public void AddProduct()
        {
            Console.WriteLine("Inserisci il nome del prodotto da aggiungere:");
            string productName = Console.ReadLine();
            Console.WriteLine("Inserisci la quantità del prodotto da aggiungere la magazzino:");
            bool validInput = false;
            int productQuantity = 0;
            while (!validInput)
            {
                validInput = int.TryParse(Console.ReadLine(), out productQuantity);
                if (!validInput)
                {
                    Console.WriteLine("Non hai inserito un numero. Riprova");
                }
            }
            Console.WriteLine("Inserisci la quantità minima che bisogna avere in magazzino per questo prodotto:");
            validInput = false;
            int productQuantityMinimum = 0;
            while (!validInput)
            {
                validInput = int.TryParse(Console.ReadLine(), out productQuantityMinimum);
                if (!validInput)
                {
                    Console.WriteLine("Non hai inserito un numero. Riprova");
                }
            }
            Product productToAdd = new Product(productName, productQuantity, productQuantityMinimum);
            _productAdoRepository.AddProduct(productToAdd);
            Console.WriteLine("Il prodotto è stato aggiunto con successo");
        }

        public void DeleteProduct()
        {
            Product productToDelete = _productService.ChooseProduct();
            _productAdoRepository.DeleteProduct(productToDelete.Id);
            Console.WriteLine("Il prodotto è stato elminato con successo");
        }

        public void ShowListProducts()
        { 
            var products = _productAdoRepository.GetProducts();
            foreach (var product in products)
            {
                Console.WriteLine($"Id: {product.Id} - Nome:  {product.Nome} - Quantità: {product.Quantita} - Quantità minima {product.QuantitaMinimaRichiesta}");
            }
        }

        public void UpdateProduct()
        {

            Console.WriteLine("Inserisci il prodotto da modificare: ");
            Product productToUpdate = _productService.ChooseProduct();
            Console.WriteLine($"Inserisci il nuovo nome del prodotto. Vecchio nome = {productToUpdate.Nome}:");
            string productName = Console.ReadLine();
            Console.WriteLine($"Inserisci la nuova quantità del prodotto. Vecchia quantità =  {productToUpdate.Quantita}:");
            bool validInput = false;
            int productQuantity = 0;
            while (!validInput)
            {
                validInput = int.TryParse(Console.ReadLine(), out productQuantity);
                if (!validInput)
                {
                    Console.WriteLine("Non hai inserito un numero. Riprova");
                }
            }
            Console.WriteLine($"Inserisci la nuova quantità minima che bisogna avere in magazzino per questo prodotto. Vecchia quantità minima = {productToUpdate.QuantitaMinimaRichiesta}:");
            validInput = false;
            int productQuantityMinimum = 0;
            while (!validInput)
            {
                validInput = int.TryParse(Console.ReadLine(), out productQuantityMinimum);
                if (!validInput)
                {
                    Console.WriteLine("Non hai inserito un numero. Riprova");
                }
            }
            productToUpdate.Nome = productName;
            productToUpdate.Quantita = productQuantity;
            productToUpdate.QuantitaMinimaRichiesta = productQuantityMinimum;
            _productAdoRepository.UpdateProduct(productToUpdate);
            Console.WriteLine("Il prodotto è stato modificato con successo");
        }
    }
}
