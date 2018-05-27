using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFaces.Product.Services
{
    public interface IBaseService<TEntity>
    {
        Guid Save(TEntity tEntity);
        bool Update(TEntity tEntity);
    }
}
