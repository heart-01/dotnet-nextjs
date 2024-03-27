using Microsoft.AspNetCore.Mvc;
using StoreAPI.Data;
using StoreAPI.Models;

namespace StoreAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    // Instance object ApplicationDbContext
    private readonly ApplicationDbContext _context;

    // Get environment
    private readonly IWebHostEnvironment _env;

    // Dependency Injection ApplicationDbContext to ProductController
    public ProductController(ApplicationDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    // GET: /api/Product/testconnectdb
    [HttpGet("testconnectdb")]
    public void TestConnection()
    {
        if (_context.Database.CanConnect())
        {
            Response.WriteAsync("Connected");
        }
        else
        {
            Response.WriteAsync("Not Connected");
        }
    }

    // GET: /api/Product
    [HttpGet]
    public ActionResult<product> GetProducts()
    {
        // LINQ get all products
        // var products = _context.products.ToList();

        // Get Product Condition
        // var products = _context.products.Where(p => p.unit_price > 45000).ToList();

        // Get Product Join Category
        var products = _context.products
            .Join(
                _context.categories,
                p => p.category_id,
                c => c.category_id,
                (p, c) => new
                {
                    p.product_id,
                    p.product_name,
                    p.unit_price,
                    p.unit_in_stock,
                    c.category_name
                }
            ).ToList();

        return Ok(products);
    }

    // GET: /api/Product/{id}
    [HttpGet("{id}")]
    public ActionResult<product> GetProduct(int id)
    {
        var product = _context.products.FirstOrDefault(p => p.product_id == id);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    // POST: /api/Product
    [HttpPost]
    public async Task<ActionResult<product>> CreateProduct([FromForm] product product, IFormFile image)
    {
        _context.products.Add(product);

        if (image != null)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            string uploadPath = Path.Combine(_env.ContentRootPath, "uploads");

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            await using (var fileStream = new FileStream(Path.Combine(uploadPath, fileName), FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }

            product.product_picture = fileName;
        }

        _context.SaveChanges();

        return Ok(product);
    }

    // PUT: /api/Product/{id}
    [HttpPut("{id}")]
    public ActionResult<product> UpdateProduct(int id, product product)
    {
        var existingProduct = _context.products.FirstOrDefault(p => p.product_id == id);

        if (existingProduct == null)
        {
            return NotFound();
        }

        existingProduct.product_name = product.product_name;
        existingProduct.unit_price = product.unit_price;
        existingProduct.unit_in_stock = product.unit_in_stock;
        existingProduct.category_id = product.category_id;

        _context.SaveChanges();

        return Ok(existingProduct);
    }

    // DELETE: /api/Product/{id}
    [HttpDelete("{id}")]
    public ActionResult<product> DeleteProduct(int id)
    {
        var product = _context.products.FirstOrDefault(p => p.product_id == id);

        if (product == null)
        {
            return NotFound();
        }

        _context.products.Remove(product);
        _context.SaveChanges();

        return Ok(product);
    }
}