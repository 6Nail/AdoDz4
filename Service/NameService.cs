using Dz4.DataAccess;
using Dz4.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dz4.Service
{
    public class NameService
    {
        private readonly string connectionString;
        private readonly string providerName;

        public NameService(string connectionString, string providerName)
        {
            this.providerName = providerName;
            this.connectionString = connectionString;
        }

        public string GetNameSeller(Guid Id)
        {
            using (var seller = new Repository<Seller>(connectionString, providerName))
            {
                var sellers = seller.GetAll();
                return sellers.SingleOrDefault(sellerq => sellerq.Id == Id).LastName;
            }
        }
        public string GetNameCustomer(Guid Id)
        {
            using (var customer = new Repository<Customer>(connectionString, providerName))
            {
                var customers = customer.GetAll();
                return customers.SingleOrDefault(customerq => customerq.Id == Id).LastName;
            }
        }
    }
}
