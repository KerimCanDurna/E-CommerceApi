using Domain.AgregateModels.CategoriModel;
using Domain.AgregateModels.ProductModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.AgregateModels.CartModel
{
    public class BasketItem
    {
        public int Id { get; set; }
        public int BasketId { get; set; }
        public int? ProductId { get; set; }
        public int Quantity { get; set; }
        virtual public DateTime CreatedDate { get; set; }
        virtual public DateTime? UpdatedDate { get; set; }
        virtual public Boolean IsActive { get; set; }

        public Basket Basket { get; set; }
        public Product Product { get; set; }
    }
}
