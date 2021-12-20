using System.Collections.Generic;
using ServerAPI.Model;

namespace ServerAPI.Builders
{
    public static class ProductResponseBuilder
    {
        public static IEnumerable<ProductResponse> GetAll()
        {
            return new List<ProductResponse>()
            {
                new ProductResponse(){ Id = 1, Name = "Teclado Mecânico Gamer", Price = 179.90M },
                new ProductResponse(){ Id = 2, Name = "GeForce RTX 3060", Price = 4000M },
                new ProductResponse(){ Id = 3, Name = "Core i9-10850K", Price = 2500M }
            };
        }
    }
}
