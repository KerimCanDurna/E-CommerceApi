using Domain.AgregateModels.CategoriModel;
using Domain.AgregateModels.ProductModel;
using Domain.IServices.IRepositories;
using Domain.Services.Repositories;


namespace Core.Repositories
{
    public class ProductRepository : EfRepositoryBase<Product, AppDbContext>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }
    }
}
