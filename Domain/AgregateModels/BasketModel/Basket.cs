using Domain.AgregateModels.UserModel;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.AgregateModels.CartModel
{
    public class Basket 
    {
        public int Id { get; set; }      
        public string? UserId { get; set; }
        virtual public bool IsActive { get; set; }
        virtual public DateTime CreatedDate { get; set; }
        virtual public DateTime ?UpdatedDate { get; set; }

        public User User { get; set; }
      
        public ICollection<BasketItem> BasketItems { get; set; }
     

    }
}
