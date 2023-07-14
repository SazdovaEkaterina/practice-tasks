using System.Text;

namespace Task1;

public class Firm
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public List<Sector> Sectors { get; set; }
    public Dictionary<Item, Employee> Inventory { get; set; }
    //item e key, employee e value
    public List<Item> Warehouse { get; set; }
    public Firm(string name)
    {
        Name = name;
        Sectors = new List<Sector>();
        Guid = Guid.NewGuid();
    }

    public void BorrowItem(Employee employee, Item item)
    {
        if (Warehouse.Contains(item)) //ako e vo magacinot, znaci nikoj ne go koristi
        {
            Inventory[item] = employee;
            Warehouse.Remove(item);
            Console.WriteLine($"The item {item} has been given to {Inventory[item].Name} {Inventory[item].LastName}.");
        } 
        else if(Inventory.ContainsKey(item) && Inventory[item] != employee.Sector.Supervisor)
        {
            Console.WriteLine($"The item {item} is being used by {Inventory[item].Name} {Inventory[item].LastName} and cannot be given to you.");
        }
        else if (Inventory.ContainsKey(item) && Inventory[item] == employee.Sector.Supervisor) //ako item-ot e kaj negoviot supervizor
        {
            Inventory[item] = employee; //dadi mu go na employee
            Console.WriteLine($"The item {item} has been given to {Inventory[item].Name} {Inventory[item].LastName}.");
        }
        else
        {
            Console.WriteLine($"The item {item} is not available in the firm.");
        }
    }

    public void ReturnItem(Employee employee, Item item)
    {
        if (Inventory.ContainsKey(item) && Inventory[item] == employee)
        {
            Console.WriteLine($"The item {item} has been returned to the warehouse by {Inventory[item].Name} {Inventory[item].LastName}.");
            Inventory.Remove(item);
            Warehouse.Add(item);
        } 
        else if (Inventory.ContainsKey(item) && Inventory[item] != employee)
        {
            Console.WriteLine($"The item {item} cannot be returned to the warehouse by {employee.Name} {employee.LastName} " +
                              $"because it's currently used by {Inventory[item].Name} {Inventory[item].LastName}.");
        }
        else if(Warehouse.Contains(item))
        {
            Console.WriteLine($"The item {item} is already in the warehouse.");
        }
        else
        {
            Console.WriteLine($"The item {item} is not available to the firm.");
        }
    }
    public void DisplayFirmInformation()
    {
        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.AppendLine($"Firm name: {Name}");

        foreach (Sector sector in Sectors)
        {
            stringBuilder.AppendLine("======================================================================");
            stringBuilder.AppendLine($"Sector: {sector.Name}");
            stringBuilder.Append(sector.Supervisor);
            
            foreach (Employee employee in sector.Employees)
            {
                stringBuilder.AppendLine("----------------------------");
                stringBuilder.Append(employee);
            }
        }

        Console.Write(stringBuilder.ToString());
    }

    public void DisplayItemsInWarehouse()
    {
            
        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.AppendLine($"\nItems in the warehouse of company {Name}");

        foreach (Item item in Warehouse)
        {
            stringBuilder.AppendLine($"{item}");
        }
        
        Console.Write(stringBuilder.ToString());
    }

    public void DisplayItemsBeingUsed()
    {
        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.AppendLine($"\nItems being used by employees in the company {Name}");

        foreach (var entry in Inventory)
        {
            stringBuilder.AppendLine($"Item {entry.Key} is being used by employee {entry.Value.Name} {entry.Value.LastName}");
        }
        
        Console.Write(stringBuilder.ToString());
    }
}