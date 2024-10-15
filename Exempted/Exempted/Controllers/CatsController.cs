using Exempted.Models;
using Exempted.Repositories;
using Microsoft.AspNetCore.Mvc;
namespace Exempted.Controllers;


[Route("api/[controller]")]
[ApiController]
public class CatsController : ControllerBase
{
    private readonly ICatRepository _catRepository;

    public CatsController(ICatRepository catRepository)
    {
        _catRepository = catRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCats()
    {
        var cats = await _catRepository.GetAllCatsAsync();
        return Ok(cats);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCatById(int id)
    {
        var cat = await _catRepository.GetCatByIdAsync(id);
        if (cat == null) return NotFound();
        return Ok(cat);
    }

    [HttpPost]
    public async Task<IActionResult> AddCat(Cat cat)
    {
        await _catRepository.AddCatAsync(cat);
        return CreatedAtAction(nameof(GetCatById), new { id = cat.Id }, cat);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCat(int id, Cat cat)
    {
        if (id != cat.Id) return BadRequest();
        await _catRepository.UpdateCatAsync(cat);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCat(int id)
    {
        await _catRepository.DeleteCatAsync(id);
        return NoContent();
    }
}