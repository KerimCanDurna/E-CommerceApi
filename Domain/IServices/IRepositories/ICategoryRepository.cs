using Domain.AgregateModels.CategoriModel;
using Domain.AgregateModels.CategoriModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IServices.IRepositories
{
    public interface ICategoryRepository : IAsyncRepository<Category>
    {
    }
}
