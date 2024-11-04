using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Model.Entities
{
    public abstract class BaseEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public DateTime CreateDate { get; set; }  
        public string? CreateUser { get; set; }    

        public DateTime? ModifiedDate { get; set; }  
        public string? ModifiedUser { get; set; }     

        public DateTime? DeletedDate { get; set; }   
        public string? DeletedUser { get; set; }
    }
}
