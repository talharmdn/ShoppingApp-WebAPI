using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Model.Entities
{
    public class CartItem : BaseEntity
    {
        public string? CartId { get; set; }  
        public string? ProductId { get; set; }  
        public int Quantity { get; set; }  

        public  virtual Product? Product { get; set; }  
        public Cart? Cart { get; set; }
    }
}
