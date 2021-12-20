using System;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Builders;

namespace ServerAPI.Controllers
{
    public class ProductController : Controller
    {
        private static int requests = 0;
        private static bool resultError = true;

        [Route("/products")]
        [HttpGet]
        public IActionResult Get()
        {
            return GetAlternateError();
        }

        [Route("/v2/products")]
        [HttpGet]
        public IActionResult GetOk()
        {
            return GetRandomError();
        }

        private IActionResult GetRandomError()
        {
            var number = new Random().Next(1, 5);
            if (number % 2 == 0)
                return Ok(ProductResponseBuilder.GetAll());

            return StatusCode(500);
        }

        private IActionResult GetAlternateError()
        {
            requests++;

            if (requests % 10 == 0)
                resultError = !resultError;

            if(resultError)
                return StatusCode(500);

            return Ok(ProductResponseBuilder.GetAll());
        }
    }
}
