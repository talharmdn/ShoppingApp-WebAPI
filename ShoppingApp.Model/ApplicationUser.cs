using Microsoft.AspNetCore.Identity;
using ShoppingApp.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; } 
        public string? PhoneNumber {  get; set; }
        public string? Role {  get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Cart> Carts { get; set; }
        public DateTime CreateDate { get; set; }
        public string? CreateUser { get; set; }

        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedUser { get; set; }

        public DateTime? DeletedDate { get; set; }
        public string? DeletedUser { get; set; }
    }
}
