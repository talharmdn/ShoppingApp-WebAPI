using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Shared.Dtos.CreateDtos
{
    public class OrderCreateDto
    {
        public string CartId { get; set; }
        public string PaymentInfo { get; set; }
        public string Address { get; set; }
        public string RecipientName { get; set; }
        public string CourierName { get; set; }
        public string CourierCompany { get; set; }
    }
}
