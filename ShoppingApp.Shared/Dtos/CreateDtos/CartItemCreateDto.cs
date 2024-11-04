using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Shared.Dtos.CreateDtos
{
    public class CartItemCreateDto
    {
        public string? ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
