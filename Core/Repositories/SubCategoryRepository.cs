using Domain.AgregateModels.CategoriModel;
using Domain.IServices.IRepositories;


namespace Core.Repositories
{
    public class SubCategoryRepository : EfRepositoryBase<SubCategory, AppDbContext>, ISubCategoryRepository
    {
        public SubCategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
