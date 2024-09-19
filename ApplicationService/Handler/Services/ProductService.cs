using Domain.CaseAktifAggregate;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Repository;
using StackExchange.Redis;

namespace ApplicationService.Handler.Services
{
    public class ProductService
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly CaseAktifDbContext _context;

        public ProductService(IConnectionMultiplexer redis, CaseAktifDbContext context)
        {
            _redis = redis;
            _context = context;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            var db = _redis.GetDatabase();
            var cachedProducts = await db.StringGetAsync("products");

            if (!string.IsNullOrEmpty(cachedProducts))
            {
                return JsonConvert.DeserializeObject<List<Product>>(cachedProducts);
            }

            var products = await _context.Product.ToListAsync();
            await db.StringSetAsync("products", JsonConvert.SerializeObject(products));

            return products;
        }

    }
}
