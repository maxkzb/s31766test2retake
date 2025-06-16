namespace retakeAPBDtest2.Models;

public class CharacterTitle
{
    public int CharacterId { get; set; }
    public int TitleId { get; set; }
    public DateTime AcquiredAt { get; set; }

    public Title Title { get; set; }
    public Character Character { get; set; }
}