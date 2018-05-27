using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFaces.Product.Repositories
{
    public interface IBaseRepository<TEntity>
    {
        Guid Save(TEntity tEntity);
        bool Update(TEntity tEntity);
    }
}
