using Domain.AgregateModels.CartModel;
using Domain.AgregateModels.CategoriModel;
using Domain.Entities;

namespace Domain.AgregateModels.ProductModel
{
    public class Product : BaseEntity
    {
        public string ProductDetail { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }

        public Category Category { get; set; }
        public ICollection<BasketItem> BasketItems { get; set; }

    }
}
