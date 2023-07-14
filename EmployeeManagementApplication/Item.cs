namespace Task1;

public class Item
{
    public Guid Guid { get; set; }
    public string Name { get; set; }

    public Item(string name)
    {
        Name = name;
        Guid = Guid.NewGuid();
    }

    public override string ToString()
    {
        return Name;
    }
}