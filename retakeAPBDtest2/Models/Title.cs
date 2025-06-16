namespace retakeAPBDtest2.Models;

public class Title
{
    public int TitleId { get; set; }
    public string Name { get; set; }
    
    public ICollection<CharacterTitle> CharacterTitles { get; set; }
}