using OrderManagementSystem.Data.Entities.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Basket
{
    public class CustomerBasket
    {
        public string Id { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }

        public List<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
        public CustomerBasket(string id)
        {
            Id = id;
        }
    }
}
