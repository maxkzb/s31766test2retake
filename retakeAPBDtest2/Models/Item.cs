namespace retakeAPBDtest2.Models;

public class Item
{
    public int ItemId { get; set; }
    public string Name { get; set; }
    public int Weight { get; set; }
    
    public ICollection<Backpack> Backpacks { get; set; }
}