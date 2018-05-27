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
    public interface IProductTranslationService : IBaseService<ProductTranslation>
    {
        ProductTranslation GetById(Guid id);
        bool Delete(Guid id);

    }
    public class ProductTranslationService : IProductTranslationService
    {
        private readonly IProductTranslationRepository _productTranslationRepository;
        public ProductTranslationService(ProductDatabaseContext productDatabaseContext, IOptions<ProductSettings> productSettings, IHttpContextAccessor iHttpContextAccessor)
        {
            _productTranslationRepository = new ProductTranslationRepository(productDatabaseContext, productSettings, iHttpContextAccessor);
        }

        public ProductTranslation GetById(Guid id)
        {
            return _productTranslationRepository.GetById(id);
        }

        public Guid Save(ProductTranslation productTranslation)
        {
            _productTranslationRepository.Save(productTranslation);
            return productTranslation.Id;
        }

        public bool Delete(Guid id)
        {
            return _productTranslationRepository.Delete(id);
        }

        public bool Update(ProductTranslation productTranslation)
        {
            return _productTranslationRepository.Update(productTranslation);
        }


    }
}
