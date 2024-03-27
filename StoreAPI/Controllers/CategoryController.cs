using Microsoft.AspNetCore.Mvc;
using StoreAPI.Data;
using StoreAPI.Models;

namespace StoreAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    // Instance object ApplicationDbContext
    private readonly ApplicationDbContext _context;

    // Get environment
    private readonly IWebHostEnvironment _env;

    // Dependency Injection ApplicationDbContext to ProductController
    public CategoryController(ApplicationDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    // GET: /api/Category
    [HttpGet]
    public ActionResult<category> GetCategories()
    {
        var categories = _context.categories.ToList();
        return Ok(categories);
    }

    // GET: /api/Category/{id}
    [HttpGet("{id}")]
    public ActionResult<category> GetCategory(int id)
    {
        var category = _context.categories.FirstOrDefault(c => c.category_id == id);

        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    // POST: /api/Category
    [HttpPost]
    public ActionResult<category> CreateCategory([FromForm] category categoryData)
    {
        _context.categories.Add(categoryData);
        _context.SaveChanges();

        return Ok(categoryData);
    }

    // PUT: /api/Category/{id}
    [HttpPut("{id}")]
    public ActionResult<category> UpdateCategory(int id, category categoryData)
    {
        var existingCategory = _context.categories.FirstOrDefault(c => c.category_id == id);

        if (existingCategory == null)
        {
            return NotFound();
        }

        existingCategory.category_id = categoryData.category_id;
        existingCategory.category_name = categoryData.category_name;
        existingCategory.category_status = categoryData.category_status;

        _context.SaveChanges();

        return Ok(existingCategory);
    }

    // DELETE: /api/Category/{id}
    [HttpDelete("{id}")]
    public ActionResult<category> DeleteProduct(int id)
    {
        var existingCategory = _context.categories.FirstOrDefault(c => c.category_id == id);

        if (existingCategory == null)
        {
            return NotFound();
        }

        _context.categories.Remove(existingCategory);
        _context.SaveChanges();

        return Ok(existingCategory);
    }
}