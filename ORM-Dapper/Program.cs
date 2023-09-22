using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace ORM_Dapper
{
    public class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");

            IDbConnection conn = new MySqlConnection(connString);

            var repo = new DapperProductRepository(conn);

            //Add item here
            //repo.CreateProduct("newItems", 20, 1);

            //Change item name
            //repo.UpdateProductName(940, "Delete Me Next");

            //Update All Product info
            var productToUpdate = repo.GetProductByID(940);

            productToUpdate.Name = "Corsair Scimitar RGB Elite";
            productToUpdate.Price = 79.99m;
            productToUpdate.CategoryID = 2;
            productToUpdate.OnSale = 1;
            productToUpdate.StockLevel = "250";

            repo.UpdateAllProductFields(productToUpdate);

            // delete items added
            //repo.DeleteProduct(941);



            var products = repo.GetAllProducts();

            foreach (var prod in products)
            {
                Console.WriteLine($"{prod.ProductID} {prod.Name}");
            }
        }
    }
}
