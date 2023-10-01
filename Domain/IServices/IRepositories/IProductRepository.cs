using Domain.AgregateModels.CategoriModel;
using Domain.IServices.IRepositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Repositories
{
    public interface IProductRepository :IAsyncRepository<Product>
    {

    }
}
