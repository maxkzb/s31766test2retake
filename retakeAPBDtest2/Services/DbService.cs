using Microsoft.EntityFrameworkCore;
using retakeAPBDtest2.Data;
using retakeAPBDtest2.DTOs;
using retakeAPBDtest2.Models;

namespace retakeAPBDtest2.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;

    public DbService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<ReturnCharacterWithIdDTO> GetCharacterWithId(int id)
    {
        var character = await _context.Characters
            .Where(c => c.CharacterId == id)
            .Select(rc => new ReturnCharacterWithIdDTO
            {
                FirstName = rc.FirstName,
                LastName = rc.LastName,
                CurrentWeight = rc.CurrentWeight,
                MaxWeight = rc.MaxWeight,
                BackpackItems = rc.Backpacks.Select(b => new BackpackItemsDTO
                {
                    ItemName = b.Item.Name,
                    ItemWeight = b.Item.Weight,
                    Amount = b.Amount
                }).ToList(),
                Titles = rc.CharacterTitles.Select(ct => new TitlesDTO
                {
                    Title = ct.Title.Name,
                    AcquiredAt = ct.AcquiredAt
                }).ToList()
            }).FirstOrDefaultAsync();
        return character;
    }

    public async Task<(bool Success, string ErrorMessage)> AddItemsToCharacter(int characterId,
        List<Item> items)
    {
        bool doesCharacterExist = await _context.Characters.AnyAsync(c => c.CharacterId == characterId);
        if (!doesCharacterExist)
        {
            return (false, $"Character with id {characterId} does not exist.");
        }

        foreach (var item in items)
        {
            bool doesItemExist = await _context.Items.AnyAsync(i => i.ItemId == item.ItemId);
            if (!doesItemExist)
            {
                return (false, $"Item with id {item.ItemId} does not exist.");
            }
        }
        
        var characterAvailableWeight = _context.Characters.Where(c => c.CharacterId == characterId).Select(c => c.MaxWeight - c.CurrentWeight);
        var sumOfItemsWeight = 0;
        foreach (var item in items)
        {
            sumOfItemsWeight += await _context.Items.Where(i => i.ItemId == item.ItemId).SumAsync(i => i.Weight);
        }

        return (true, null);
    }
}