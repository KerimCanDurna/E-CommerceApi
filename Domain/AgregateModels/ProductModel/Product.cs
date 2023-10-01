using Domain.AgregateModels.CartModel;
using Domain.Entities;

namespace Domain.AgregateModels.CategoriModel
{
    public class Product : BaseEntity
    {
        public string ProductDetail { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int SubCategoryId { get; set; }

        public SubCategory SubCategory { get; set; }
        public ICollection<BasketItem> BasketItems { get; set; }

    }
}
