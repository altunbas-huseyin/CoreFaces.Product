using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using CoreFaces.Product.Models.Domain;
using CoreFaces.Product.Models.Models;
using CoreFaces.Product.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFaces.Product.Services
{
    public interface IBrandService : IBaseService<Brand>
    {
        CoreFaces.Product.Models.Domain.Brand GetById(Guid id, Guid apiUserId);
        bool Delete(Guid id, Guid apiUserId);
    }
    public class BrandService : IBrandService
    {
        private readonly ProductDatabaseContext _productDatabaseContext;
        private readonly IBrandRepository _brandRepository;
        public BrandService(ProductDatabaseContext productDatabaseContext, IOptions<ProductSettings> productSettings, IHttpContextAccessor iHttpContextAccessor)
        {
            _productDatabaseContext = productDatabaseContext;
            _brandRepository = new BrandRepository(productDatabaseContext, productSettings, iHttpContextAccessor);
        }


        public Brand GetById(Guid id, Guid apiUserId)
        {
            return _brandRepository.GetById(id, apiUserId);
        }

        public bool Delete(Guid id, Guid apiUserId)
        {
            return _brandRepository.Delete(id, apiUserId);
        }

        public bool Update(Brand Brand)
        {
            return _brandRepository.Update(Brand);
        }

        public Guid Save(Brand model)
        {
            model.Id = _brandRepository.Save(model);
            return model.Id;
        }

    }
}
