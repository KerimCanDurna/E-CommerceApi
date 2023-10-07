using Domain.AgregateModels.CategoriModel;
using Domain.IServices.IRepositories;


namespace Core.Repositories
{
    public class CategoryRepository : EfRepositoryBase<Category, AppDbContext>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
