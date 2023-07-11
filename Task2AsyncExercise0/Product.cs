using System.Text;
namespace Task2AsyncExercise0;

public class Product
{
    public string Name { get; set; }
    public double Price { get; set; }
    public int Stock { get; set; }

    public Product(string name, double price, int stock)
    {
        Name = name;
        Price = price;
        Stock = stock;
    }

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine($"Product name: {Name}");
        stringBuilder.AppendLine($"Price: ${Price:0.00}");
        stringBuilder.AppendLine($"Items in stock: {Stock}");
        return stringBuilder.ToString();
    }
}