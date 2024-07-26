using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _catalogContext;
        public ProductRepository(ICatalogContext catalogContext)
        {
            _catalogContext = catalogContext 
            ?? throw new ArgumentNullException(nameof(catalogContext));
        }
        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            try
            {
                FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);

                return await _catalogContext
                            .Products
                            .Find(filter)
                            .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("The product By name {@name} cannot be find", ex);
            }
            return null;
        }
        public async Task<Product> GetProduct(Guid id)
        {
            try
            {
                return await _catalogContext
                            .Products.Find(p => p.Id == id)
                            .FirstOrDefaultAsync();    
            }
            catch (Exception ex)
            {
                Console.WriteLine("The product {@Id} could not be found", ex);
            }
            return null;
        }
        public async Task<IEnumerable<Product>> GetProducts()
        {
            try
            {
                return await _catalogContext
                                .Products
                                .Find(p => true)
                                .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not get all products from the database.", ex);
            }

            return null;    
        } 
        public async Task CreateProduct(Product product)
        {
            try
            {
                 await _catalogContext
                                .Products
                                .InsertOneAsync(product);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Could not find {product}", ex);
            };

        }

        public async Task<bool> DeleteProduct(Guid id)
        {
            try
            {
                FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);

                DeleteResult deleteResult = await _catalogContext
                                                        .Products
                                                        .DeleteOneAsync(filter);

                return deleteResult.IsAcknowledged
                    && deleteResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
            }
            return false;
        }


        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            try
            {
                FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, categoryName);

                return await _catalogContext
                                .Products
                                .Find(filter)
                                .ToListAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Could not find {categoryName}", ex);
            }
            return null;
        }
          
        public async Task<bool> UpdateProduct(Product product)
        {
            try
            {
                var updateResult = await _catalogContext
                              .Products
                                 .ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);

                return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not update {product}", ex);
            }
            return false;
        }
    }
}
