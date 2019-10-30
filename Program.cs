using Dz4.DataAccess;
using Dz4.Domain;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dz4
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appSettings.json", false, true);
            IConfigurationRoot configurationRoot = builder.Build();

            var providerName = configurationRoot.GetSection("AppConfig").GetChildren().Single(item => item.Key == "ProviderName").Value;
            var connectionString = configurationRoot.GetConnectionString("MyConnectionString");

            //List<Customer> customers;
            //List<Seller> sellers;
            //using (var context = new Repository<Seller>(connectionString, providerName))
            //{
            //    var sellerFirst = new Seller
            //    {
            //        FirstName = "Антон",
            //        LastName = "Пориченко"
            //    };
            //    var sellerSecond = new Seller
            //    {
            //        FirstName = "Иван",
            //        LastName = "Таксебе"
            //    };
            //    context.Add(sellerFirst);
            //    context.Add(sellerSecond);
            //    sellers = context.GetAll().ToList();
            //}
            //using (var context = new Repository<Customer>(connectionString, providerName))
            //{
            //    var customerFirst = new Customer
            //    {
            //        FirstName = "Екатерина",
            //        LastName = "Продажная"
            //    };
            //    var customerSecond = new Customer
            //    {
            //        FirstName = "Выхухоль",
            //        LastName = "Обычная"
            //    };
            //    context.Add(customerFirst);
            //    context.Add(customerSecond);
            //    customers = context.GetAll().ToList();
            //}
            //using (var context = new Repository<Sale>(connectionString, providerName))
            //{
            //    var saleFirst = new Sale
            //    {
            //        CustomerId = customers.First().Id,
            //        SellerId = sellers.First().Id,
            //        Price = 1200,
            //        SaleDate = DateTime.Now
            //    };
            //    var saleSecond = new Sale
            //    {
            //        CustomerId = customers.Last().Id,
            //        SellerId = sellers.Last().Id,
            //        Price = 1000,
            //        SaleDate = DateTime.Now
            //    };
            //    context.Add(saleFirst);
            //    context.Add(saleSecond);
            //}

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Sales(connectionString, providerName));
        }
    }
}
