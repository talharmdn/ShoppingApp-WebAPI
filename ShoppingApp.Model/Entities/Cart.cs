using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Model.Entities
{
    public class Cart : BaseEntity
    {
        public ICollection<CartItem>? Items { get; set; }
    }
}
