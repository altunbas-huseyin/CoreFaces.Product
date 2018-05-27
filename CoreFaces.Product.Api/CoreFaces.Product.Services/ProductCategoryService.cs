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
    public interface IProductCategoryService : IBaseService<ProductCategory>
    {
        CoreFaces.Product.Models.Domain.ProductCategory GetById(Guid id, Guid apiUserId);
        bool Delete(Guid id, Guid apiUserId);
        ProductCategory ProductCategoryViewToProductCategory(ProductCategoryView productCategoryView);
        ProductCategoryView Save(ProductCategoryView productCategoryView);
    }
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly ProductDatabaseContext _productDatabaseContext;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IProductCategoryTranslationRepository _productCategoryTranslationRepository;
        public ProductCategoryService(ProductDatabaseContext productDatabaseContext, IOptions<ProductSettings> productSettings, IHttpContextAccessor iHttpContextAccessor)
        {
            _productDatabaseContext = productDatabaseContext;
            _productCategoryRepository = new ProductCategoryRepository(productDatabaseContext, productSettings, iHttpContextAccessor);
            _productCategoryTranslationRepository = new ProductCategoryTranslationRepository(productDatabaseContext, productSettings, iHttpContextAccessor);
        }


        public ProductCategory GetById(Guid id, Guid apiUserId)
        {
            return _productCategoryRepository.GetById(id, apiUserId);
        }

        public Guid Save(ProductCategory productCategory)
        {
            _productCategoryRepository.Save(productCategory);
            return productCategory.Id;
        }
        public ProductCategory ProductCategoryViewToProductCategory(ProductCategoryView productCategoryView)
        {
            ProductCategory productCategory = new ProductCategory();
            productCategory.Id = productCategoryView.Id;
            productCategory.ParentId = productCategoryView.ParentId;
            productCategory.ApiUserId = productCategoryView.ApiUserId;
            productCategory.Name = productCategoryView.Name;
            productCategory.Description = productCategoryView.Description;
            productCategory.CreateDate = productCategoryView.CreateDate;
            productCategory.UpdateDate = productCategoryView.UpdateDate;

            return productCategory;
        }

        public ProductCategoryView Save(ProductCategoryView productCategoryView)
        {
            ProductCategory productCategory = this.ProductCategoryViewToProductCategory(productCategoryView);
            _productDatabaseContext.Database.BeginTransaction();
            try
            {
                productCategoryView.Id = _productCategoryRepository.Save(productCategory);
                for (int i = 0; i < productCategoryView.ProductCategoryTranslationList.Count; i++)
                {
                    productCategoryView.ProductCategoryTranslationList[i].ProductCategoryId = productCategoryView.Id;
                    productCategoryView.ProductCategoryTranslationList[i].Id = _productCategoryTranslationRepository.Save(productCategoryView.ProductCategoryTranslationList[i]);
                }
                _productDatabaseContext.Database.CommitTransaction();
            }
            catch (Exception ex)
            {
                _productDatabaseContext.Database.RollbackTransaction();
                throw;
            }
            return productCategoryView;
        }
        public bool Delete(Guid id, Guid apiUserId)
        {
            return _productCategoryRepository.Delete(id, apiUserId);
        }

        public bool Update(ProductCategory productCategory)
        {
            return _productCategoryRepository.Update(productCategory);
        }
    }
}
