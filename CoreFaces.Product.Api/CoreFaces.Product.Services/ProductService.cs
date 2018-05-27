using CoreFaces.Product.Models.Domain;
using CoreFaces.Product.Models.Models;
using CoreFaces.Product.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Kendo.DynamicLinq;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;


namespace CoreFaces.Product.Services
{
    public interface IProductService : IBaseService<Models.Domain.Product>
    {
        Guid Save(ProductView product);
        Models.Domain.Product GetProductId(Guid Id, Guid apiUserId);
        //Models.Domain.Product ProductViewToProduct(ProductView productView);
        List<ProductView> GetProductViewList(Guid apiUserId, Kendo.DynamicLinq.View filters);
        ProductView GetProductViewByProductId(Guid Id, Guid apiUserId);
    }
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductTranslationRepository _productTranslationRepository;
        private readonly ProductDatabaseContext _productDatabaseContext;
        private const string tableName = "Product";
        private readonly IOptions<ProductSettings> _productSettings;
        public ProductService(ProductDatabaseContext productDatabaseContext, IOptions<ProductSettings> productSettings, IHttpContextAccessor iHttpContextAccessor)
        {
            _productSettings = productSettings;
            _productDatabaseContext = productDatabaseContext;
            _productRepository = new ProductRepository(productDatabaseContext, productSettings, iHttpContextAccessor);
            _productTranslationRepository = new ProductTranslationRepository(productDatabaseContext, productSettings, iHttpContextAccessor);
        }

        public Models.Domain.Product GetById(Guid id, Guid apiUserId)
        {
            return _productRepository.GetById(id, apiUserId);
        }

        public Guid Save(Models.Domain.Product product)
        {
            _productRepository.Save(product);
            return product.Id;
        }

        //public Models.Domain.Product ProductViewToProduct(ProductView productView)
        //{
        //    Models.Domain.Product product = new Models.Domain.Product();
        //    product.Id = productView.Id;
        //    product.UserId = productView.UserId;
        //    product.Name = productView.Name;
        //    product.Price = productView.Price;
        //    product.Vat = productView.Vat;
        //    product.Currency = productView.Currency;
        //    product.CreateDate = productView.CreateDate;
        //    product.UpdateDate = productView.UpdateDate;

        //    return product;
        //}

        public Guid Save(CoreFaces.Product.Models.Models.ProductView productView)
        {
            _productDatabaseContext.Database.BeginTransaction();
            Models.Domain.Product product = productView.Product;
            try
            {
                #region Ürün kaydı yapılıyor
                product.Id = _productRepository.Save(product);
                //ürüne ait ProductTranslation kaydı yapılıyor türkçe, ingilizce vb.
                for (int i = 0; i < productView.ProductTranslationList.Count; i++)
                {
                    productView.ProductTranslationList[i].ProductId = product.Id;
                    productView.ProductTranslationList[i].Id = _productTranslationRepository.Save((ProductTranslation)productView.ProductTranslationList[i]);
                }
                #endregion


                //herhangi bir hata yok ise database kayıt işlemleri commit ediliyor.
                _productDatabaseContext.Database.CommitTransaction();
            }
            catch (Exception ex)
            {
                //Hata var ise database işlemleri geri alınıyor.
                _productDatabaseContext.Database.RollbackTransaction();
                throw;
            }

            return product.Id;
        }

        public bool Delete(Guid id, Guid apiUserId)
        {
            return _productRepository.Delete(id, apiUserId);
        }

        public bool Update(Models.Domain.Product product)
        {
            return _productRepository.Update(product);
        }

        public Models.Domain.Product GetProductId(Guid Id, Guid apiUserId)
        {
            CoreFaces.Product.Models.Domain.Product product = _productRepository.GetById(Id, apiUserId);
            return product;
        }

        public ProductView GetProductViewByProductId(Guid Id, Guid apiUserId)
        {
            CoreFaces.Product.Models.Domain.Product product = _productRepository.GetById(Id, apiUserId);
            List<ProductTranslation> productTranslation = _productTranslationRepository.GetByProductId(product.Id);

            ProductView productView = new ProductView();
            productView.Product = product;
            productView.ProductTranslationList = productTranslation;

            return productView;
        }

        public List<ProductView> GetProductViewList(Guid apiUserId, View filters)
        {
            List<ProductView> productViewList = new List<ProductView>();
            List<Models.Domain.Product> productList = _productRepository.Get(filters);
            foreach (Models.Domain.Product product in productList)
            {
                ProductView productView = GetProductViewByProductId(product.Id, apiUserId);
                productViewList.Add(productView);
            }
            return productViewList;
        }

    }
}
