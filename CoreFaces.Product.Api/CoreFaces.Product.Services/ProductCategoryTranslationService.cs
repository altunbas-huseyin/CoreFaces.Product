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
    public interface IProductCategoryTranslationService : IBaseService<ProductCategoryTranslation>
    {
        CoreFaces.Product.Models.Domain.ProductCategoryTranslation GetById(Guid id);
        bool Delete(Guid id);
    }
    public class ProductCategoryTranslationService : IProductCategoryTranslationService
    {
        private readonly IProductCategoryTranslationRepository _productCategoryTranslationRepository;
        public ProductCategoryTranslationService(ProductDatabaseContext productDatabaseContext, IOptions<ProductSettings> productSettings, IHttpContextAccessor iHttpContextAccessor)
        {
            _productCategoryTranslationRepository = new ProductCategoryTranslationRepository(productDatabaseContext, productSettings, iHttpContextAccessor);
        }


        public ProductCategoryTranslation GetById(Guid id)
        {
            return _productCategoryTranslationRepository.GetById(id);
        }

        public Guid Save(ProductCategoryTranslation productCategoryTranslation)
        {
            _productCategoryTranslationRepository.Save(productCategoryTranslation);
            return productCategoryTranslation.Id;
        }

        public bool Delete(Guid id)
        {
            return _productCategoryTranslationRepository.Delete(id);
        }

        public bool Update(ProductCategoryTranslation productCategoryTranslation)
        {
            return _productCategoryTranslationRepository.Update(productCategoryTranslation);

        }

    }
}
