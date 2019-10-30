using Dz4.DataAccess;
using Dz4.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dz4.Service
{
    public class SelectedItemService
    {
        private readonly string connectionString;
        private readonly string providerName;
        public SelectedItemService(string connectionString, string providerName)
        {
            this.providerName = providerName;
            this.connectionString = connectionString;
        }

        public void GetSellers(TextBox text)
        {
            using (var context = new Repository<Seller>(connectionString,providerName))
            {
                var sellers = context.GetAll().ToList();
                foreach (var seller in sellers)
                {
                    text.Text += $"Имя: {seller.FirstName}\r\nФамилия: {seller.LastName}\r\n\r\n";
                }
            }
        }
        public void GetCustomers(TextBox text)
        {
            using (var context = new Repository<Customer>(connectionString, providerName))
            {
                var customers = context.GetAll().ToList();
                foreach (var customer in customers)
                {
                    text.Text += $"Имя: {customer.FirstName}\r\nФамилия: {customer.LastName}\r\n\r\n";
                }
            }
        }

        public void GetSales(TextBox text)
        {
            NameService name = new NameService(connectionString, providerName);
            using (var context = new Repository<Sale>(connectionString, providerName))
            {
                var sales = context.GetAll().ToList();
                foreach (var sale in sales)
                {
                    text.Text += $"Покупатель: {name.GetNameCustomer(sale.CustomerId)}\r\nПродавец: {name.GetNameSeller(sale.SellerId)}\r\n" +
                        $"Цена: {sale.Price}\r\nДата сделки: {sale.SaleDate}\r\n\r\n";
                }
            }
        }
    }
}
