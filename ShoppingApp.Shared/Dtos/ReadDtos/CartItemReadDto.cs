using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Shared.Dtos.ReadDtos
{
    public class CartItemReadDto
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public DateTime CreateDate { get; set; }
        public string? CreateUser { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedUser { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string? DeletedUser { get; set; }
    }

}
