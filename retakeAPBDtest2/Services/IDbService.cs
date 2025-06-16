using retakeAPBDtest2.DTOs;
using retakeAPBDtest2.Models;

namespace retakeAPBDtest2.Services;

public interface IDbService
{
    Task<ReturnCharacterWithIdDTO> GetCharacterWithId(int id);
    Task<(bool Success, string ErrorMessage)> AddItemsToCharacter(int characterId, List<Item> items);
}