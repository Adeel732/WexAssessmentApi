using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductsController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var products = await _productRepository.GetAllAsync();
        var pagedProducts = products.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        return Ok(pagedProducts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] Product product)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        await _productRepository.AddAsync(product);
        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product updatedProduct)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null) return NotFound();

        await _productRepository.UpdateAsync(updatedProduct);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null) return NotFound();

        await _productRepository.DeleteAsync(id);
        return NoContent();
    }
}
