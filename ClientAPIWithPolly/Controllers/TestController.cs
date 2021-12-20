using System.Threading.Tasks;
using ClientAPIWithPolly.Services;
using Microsoft.AspNetCore.Mvc;
using Polly.CircuitBreaker;

namespace ClientAPIWithPolly.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IProductService productService;
        public TestController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            try
            {
                return await productService.GetProductsAsync();
            }
            catch (System.Exception ex)
            {
                if (ex is BrokenCircuitException) return "Circuit Breaker Ativado.";
                throw;
            }
        }

        [HttpGet]
        [Route("/v2/Test")]
        public async Task<string> GetOk()
        {
            try
            {
                return await productService.GetProductsV2Async();
            }
            catch (System.Exception ex)
            {
                if (ex is BrokenCircuitException) return "Circuit Breaker Ativado.";
                throw;
            }
        }
    }
}
