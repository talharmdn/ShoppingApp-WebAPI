using ShoppingApp.Shared.Dtos.ReadDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Shared.Dtos.UpdateDtos
{
    public class CartUpdateDto
    {
        public string? Id { get; set; }
        public List<CartItemUpdateDto>? Items { get; set; }
    }

}
