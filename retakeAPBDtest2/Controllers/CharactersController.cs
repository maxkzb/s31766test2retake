using Microsoft.AspNetCore.Mvc;
using retakeAPBDtest2.DTOs;
using retakeAPBDtest2.Models;
using retakeAPBDtest2.Services;

namespace retakeAPBDtest2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CharactersController : ControllerBase
{
    private readonly IDbService _dbService;

    public CharactersController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet("characters/{characterId}")]
    public async Task<ActionResult<IEnumerable<Character>>> GetCharacters(int characterId)
    {
        if (characterId <= 0)
        {
            return BadRequest();
        }

        try
        {
            var character = await _dbService.GetCharacterWithId(characterId);
            if (character == null)
            {
                return NotFound();
            }

            return Ok(character);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}