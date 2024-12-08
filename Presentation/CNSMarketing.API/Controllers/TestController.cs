using CNSMarketing.Application.Abstraction.ExternalService.Common;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CNSMarketing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IMailService _mailService;


        public TestController(IMapper mapper, IMailService mailService)
        {
            _mapper = mapper;
            _mailService = mailService;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var productManager = new ProductManager();

            var products = productManager.GetProducts();

            var productDto = _mapper.Map<List<ProductDto>>(products);

            return Ok(productDto);
        }

        [HttpGet("GetMail")]
        public async Task<IActionResult> GetMail()
        {
            await _mailService.SendMailAsync("zng.caferaydin@gmail.com", "Test", "Deneme");

            return Ok();
        }

        public class ProductManager
        {
            public List<Product> GetProducts()
            {

                var category = new List<Category>()
                {
                    new Category { Id = 1, Name= "Telefon"},
                    new Category { Id = 2, Name= "Bilgisayar"},

                };

                var products = new List<Product>()
                {
                    new Product { Id = 1, Name = "Samsung", CategoryId = 1 },
                    new Product { Id = 2, Name = "Apple", CategoryId = 1 },
                    new Product { Id = 3, Name = "Lenova", CategoryId = 2 },
                    new Product { Id = 4, Name = "Monster", CategoryId = 2 },
                };

                return products;
            }
        }

        public class Product
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public int CategoryId { get; set; }
            public Category Category { get; set; }
        }

        public class ProductDto
        {
            public string? Name { get; set; }
            public int CategoryId { get; set; }
        }

        public class Category
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
