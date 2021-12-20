using System.Threading.Tasks;

namespace ClientAPIWithPolly.Services
{
    public interface IProductService
    {
        Task<string> GetProductsAsync();
        Task<string> GetProductsV2Async();
    }
}