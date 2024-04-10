using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CategoryController : BaseController
{
    private readonly IGenericRepository<Category> _categoryRepo;
    public CategoryController(IGenericRepository<Category> categoryRepo)
    {
        _categoryRepo = categoryRepo;
    }

    [HttpGet]
    public async Task<ActionResult<List<Category>>> GetCategories()
    {
        var categories = await _categoryRepo.ListAllAsync();
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategory(int id)
    {
        var category = await _categoryRepo.GetByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    [HttpPost("add-category")]
    public async Task<ActionResult<Category>> AddCategory(Category category)
    {
        var newCategory = await _categoryRepo.AddAsync(category);
        return Ok(newCategory);
    }

    [HttpPut("update-category")]
    public async Task<ActionResult<Category>> UpdateCategory(Category category)
    {
        await _categoryRepo.UpdateAsync(category);
        return Ok(category);
    }

    [HttpDelete("delete-category/{id}")]
    public async Task<ActionResult<Category>> DeleteCategory(int id)
    {
        await _categoryRepo.DeleteAsync(id);
        return Ok();
    }

}
