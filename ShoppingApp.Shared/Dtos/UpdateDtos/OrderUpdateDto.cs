using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Shared.Dtos.UpdateDtos
{
    public class OrderUpdateDto
    {
        public string? Id { get; set; }
        public string? PaymentInfo { get; set; }
        public string? Address { get; set; }
        public string? RecipientName { get; set; }
        public string? CourierName { get; set; }
        public string? CourierCompany { get; set; }
    }
}
