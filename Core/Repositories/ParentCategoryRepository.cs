using Domain.AgregateModels.CategoriModel;
using Domain.IServices.IRepositories;


namespace Core.Repositories
{
    public class ParentCategoryRepository : EfRepositoryBase<ParentCategory, AppDbContext>, IParentCategoryRepository
    {
        public ParentCategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
