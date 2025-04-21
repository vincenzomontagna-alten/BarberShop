using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBLayer.Interfaces;
using DBLayer.Models;

namespace ServiceLayer
{
    public class ProductService
    {
        private readonly IProductRepositoy _productRepositoy;
        public ProductService(IProductRepositoy productRepository) 
        {
            _productRepositoy = productRepository;
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
                    case "5":
                        ShowProductsToBuy();
                        break;
                    default:
                        Console.WriteLine("Scelta non valida");
                        break;


                }

            }
        }
        public void ShowProductsToBuy()
        { 
            var productsToBuy = _productRepositoy.GetProducts().Where(p => p.Quantita < p.QuantitaMinimaRichiesta);
            Console.WriteLine("I prodotti da comprare sono:");
            foreach (var product in productsToBuy)
            {
                Console.WriteLine(product.Nome + $" - quantità prevista: {product.QuantitaMinimaRichiesta} - quantità posseduta: {product.Quantita}");
            }
        }
        public Product ChooseProduct()
        {
            ShowListProducts();
            
            bool validInput = false;
            var productsIds = _productRepositoy.GetProducts().Select(p => p.Id).ToList();
            int productId = 0;
            while (!validInput)
            {
                validInput = int.TryParse(Console.ReadLine(), out productId);
                if (!validInput)
                {
                    Console.WriteLine("Non hai inserito un numero. Riprova");
                    continue;
                }
                if (!productsIds.Contains(productId))
                {
                    Console.WriteLine("L' id inserito non è contenuto nel db. Riprova");
                    validInput = false;                
                }
                
            }

            return _productRepositoy.GetProduct(productId);
        }

        public void UpdateProduct()
        {
            Console.WriteLine("Inserisci il prodotto da modificare: ");
            Product productToUpdate = ChooseProduct();
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
            productToUpdate.QuantitaMinimaRichiesta =productQuantityMinimum;
            _productRepositoy.UpdateProduct(productToUpdate);
        }
        public void DeleteProduct()
        {
            Console.WriteLine("Inserisci il prodotto da eliminare: ");
            Product productToDelete = ChooseProduct();
            _productRepositoy.DeleteProduct(productToDelete.Id);
            Console.WriteLine("Prodotto eliminato con successo");
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
            Product productToAdd = new Product(productName,productQuantity, productQuantityMinimum);
            _productRepositoy.AddProduct(productToAdd);
            Console.WriteLine("Il prodotto è stato aggiunto con successo");
        }

        public void ShowListProducts()
        { 
            var products = _productRepositoy.GetProducts();
            foreach (var product in products)
            {
                Console.WriteLine("Id: " + product.Id + " - Nome: " + product.Nome + " - Quantità: " + product.Quantita + " - Quantità minima richiesta: " + product.QuantitaMinimaRichiesta);
            }
        }
    }
}
