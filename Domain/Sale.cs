using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dz4.Domain
{
    public class Sale : Entity
    {
        public Guid SellerId { get; set; }
        public Guid CustomerId { get; set; }
        public double Price { get; set; }
        public DateTime SaleDate { get; set; }
    }
}
