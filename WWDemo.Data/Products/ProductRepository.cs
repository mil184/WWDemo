using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using WWDemo.Models;

namespace WWDemo.Data.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApiDbContext _apiDbContext;

        public ProductRepository(ApiDbContext apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }

        public async Task<List<Product?>> GetAllProducts()
        {
            return await Task.Run(() => GetQueryable().ToList());
        }

        public Task<Product?> GetProductById(Guid productId)
        {
            return GetQueryable().FirstOrDefaultAsync(x => x!.Id == productId);
        }

        public async Task<Product?> AddProduct(Product product)
        {
            var result = _apiDbContext.Products?.Add(product)!;

            await _apiDbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<Product?> UpdateProduct(Product product)
        {
            var result = _apiDbContext.Products?.Update(product)!;

            await _apiDbContext.SaveChangesAsync();

            return result.Entity;
        }

        private IQueryable<Product?> GetQueryable()
        {
            var products = _apiDbContext.Products;

            return products;
        }

        private Task<Product?> GetProductBySerialNumber(string serialNumber)
        {
            return GetQueryable().FirstOrDefaultAsync(x => x!.SerialNumber == serialNumber);
        }

        Task<Product?> IProductRepository.GetProductBySerialNumber(string serialNumber)
        {
            return GetProductBySerialNumber(serialNumber);
        }

        private async Task<List<Product?>> GetProductsByType(string type)
        {
            // var products = (await _productRepository.GetAllProducts()).Where(p => p.Type == request.Type).ToList(); 

            //var products = await _productRepository.GetAllProducts();
            //List<Product> productsByType = new List<Product>();
            //foreach (Product p in products)
            //{

            //    if (p.GetType() == request.GetType())
            //    {
            //        productsByType.Add(p);
            //    }
            //}

            var products = await GetAllProducts();
            var productsByType = products.Where(p => p.Type == type).ToList();
            return productsByType;
        }

        Task<List<Product?>> IProductRepository.GetProductsByType(string type)
        {
            return GetProductsByType(type);
        }

        private Task<Product?> GetProductByName(string name)
        {
            return GetQueryable().FirstOrDefaultAsync(x => x!.Name == name);
        }

        Task<Product?> IProductRepository.GetProductByName(string name)
        {
            return GetProductByName(name);
        }
    }
}
 