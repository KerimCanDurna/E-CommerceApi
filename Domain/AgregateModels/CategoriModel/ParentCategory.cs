using Domain.Entities;

namespace Domain.AgregateModels.CategoriModel
{
    public class ParentCategory : BaseEntity
    {

       
        public virtual ICollection<ParentCategory>? SubCategories { get; set; }

        

    }


}
