using Domain.Entities;


namespace Domain.AgregateModels.CategoriModel
{
    public class SubCategory : BaseEntity
    {
        public int? ParentCategoryId { get; set; }

        public virtual  ParentCategory? ParentCategory { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
    }


}
