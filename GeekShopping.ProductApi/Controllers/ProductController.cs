using GeekShopping.ProductApi.Data.ValueObjects;
using GeekShopping.ProductApi.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.ProductApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        [Route("find-all")]
        public async Task<ActionResult<IEnumerable<ProductVO>>> FindAll()
        {
            return Ok(await _productRepository.FindAll());
        }

        [HttpGet]
        [Route("find-by-id/{id:long}")]
        public async Task<ActionResult<ProductVO>> FindById(long id)
        {
            var product = await _productRepository.FindById(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductVO>> Create(ProductVO productVO)
        {
            if (productVO == null) return BadRequest();
            var product = await _productRepository.Create(productVO);
            return Ok(product);
        }

        [HttpPut]
        public async Task<ActionResult<ProductVO>> Update(ProductVO productVO)
        {
            if (productVO == null) return BadRequest();
            var product = await _productRepository.Update(productVO);
            return Ok(product);
        }

        [HttpDelete]
        [Route("{id:long}")]
        public async Task<ActionResult> FindAll(long id)
        {
            var status = await _productRepository.Delete(id);
            if (!status) return BadRequest(status);
            return Ok(status);
        }
    }
}
