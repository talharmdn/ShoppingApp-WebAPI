using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Shared.Dtos.ReadDtos
{
    public class OrderReadDto
    {
        public string? Id { get; set; }
        public string? PaymentInfo { get; set; }
        public string? Address { get; set; }
        public string? RecipientName { get; set; }
        public string? CourierName { get; set; }
        public string? CourierCompany { get; set; }
        public CartReadDto? Cart { get; set; }
        public DateTime CreateDate { get; set; }
        public string? CreateUser { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedUser { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string? DeletedUser { get; set; }
    }
}
