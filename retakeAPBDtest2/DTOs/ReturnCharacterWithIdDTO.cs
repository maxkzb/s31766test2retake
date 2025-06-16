namespace retakeAPBDtest2.DTOs;

public class ReturnCharacterWithIdDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int CurrentWeight { get; set; }
    public int MaxWeight { get; set; }
    public List<BackpackItemsDTO> BackpackItems { get; set; }
    public List<TitlesDTO> Titles { get; set; }
}

public class BackpackItemsDTO
{
    public string ItemName { get; set; }
    public int ItemWeight { get; set; }
    public int Amount { get; set; }
}

public class TitlesDTO
{
    public string Title { get; set; }
    public DateTime AcquiredAt { get; set; }
}