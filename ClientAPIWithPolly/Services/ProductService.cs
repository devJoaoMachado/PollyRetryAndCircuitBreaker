using System.Net.Http;
using System.Threading.Tasks;

namespace ClientAPIWithPolly.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory httpClientFactory;
        public ProductService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<string> GetProductsAsync()
        {
            var result = await httpClientFactory.CreateClient("ProductService").GetAsync("/products");

            if (result.IsSuccessStatusCode)
                return await result.Content.ReadAsStringAsync();

            return result.ReasonPhrase;
        }

        public async Task<string> GetProductsV2Async()
        {
            var result = await httpClientFactory.CreateClient("ProductServiceV2").GetAsync("/v2/products");

            if (result.IsSuccessStatusCode)
                return await result.Content.ReadAsStringAsync();

            return result.ReasonPhrase;
        }
    }
}
