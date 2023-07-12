using System.Text;
using Task1;

//FIRM & SECTORS
Firm firm = new Firm("A & B Inc.");

Sector sector1 = new Sector("Finances");
Sector sector2 = new Sector("Production");
Sector sector3 = new Sector("Human Resources");

firm.Sectors.Add(sector1);
firm.Sectors.Add(sector2);
firm.Sectors.Add(sector3);

//ITEMS
Item item1 = new Item("Notebook");
Item item2 = new Item("Pen");
Item item3 = new Item("Desk");
Item item4 = new Item("Chair");
Item item5 = new Item("Monitor");

//FINANCES SECTOR
Employee employee1_1 = new Employee("Jon", "Smith",
    DateTime.Parse("5/1/1998 8:30:52 AM", System.Globalization.CultureInfo.InvariantCulture),
    DateTime.Parse("1/6/2008 8:30:52 AM", System.Globalization.CultureInfo.InvariantCulture),
    19.99,
    Currency.EUR,
    sector1);
employee1_1.Inventory.Add(item1);
employee1_1.Inventory.Add(item2);
    
Employee employee1_2 = new Employee("Ana", "Green",
    DateTime.Parse("12/12/1995 8:30:52 AM", System.Globalization.CultureInfo.InvariantCulture),
    DateTime.Parse("9/4/2014 8:30:52 AM", System.Globalization.CultureInfo.InvariantCulture),
    39.99,
    Currency.EUR,
    sector1);

Employee employee1_3 = new Employee("Hana", "Brown",
    DateTime.Parse("12/12/1995 8:30:52 AM", System.Globalization.CultureInfo.InvariantCulture),
    DateTime.Parse("7/7/2023 8:30:52 AM", System.Globalization.CultureInfo.InvariantCulture),
    59.99,
    Currency.USD,
    sector1);

Supervisor supervisor1 = new Supervisor("Jane", "Smith",
    DateTime.Parse("12/12/1995 8:30:52 AM", System.Globalization.CultureInfo.InvariantCulture),
    DateTime.Parse("9/4/2014 8:30:52 AM", System.Globalization.CultureInfo.InvariantCulture),
    99.99,
    Currency.EUR,
    sector1,
    "jane-smith@gmail.com");
supervisor1.Inventory.Add(item3);
supervisor1.Inventory.Add(item4);
supervisor1.Inventory.Add(item5);

sector1.Employees.Add(employee1_1);
sector1.Employees.Add(employee1_2);
sector1.Employees.Add(employee1_3);
sector1.Supervisor = supervisor1;

//PRODUCTION SECTOR
Employee employee2_1 = new Employee("Tom", "Black",
    DateTime.Parse("5/1/2001 8:30:52 AM", System.Globalization.CultureInfo.InvariantCulture),
    DateTime.Parse("1/6/2018 8:30:52 AM", System.Globalization.CultureInfo.InvariantCulture),
    15.99,
    Currency.USD,
    sector2);
    
Employee employee2_2 = new Employee("Maya", "May",
    DateTime.Parse("10/10/1985 8:30:52 AM", System.Globalization.CultureInfo.InvariantCulture),
    DateTime.Parse("5/6/2003 8:30:52 AM", System.Globalization.CultureInfo.InvariantCulture),
    89.99,
    Currency.EUR,
    sector2);

Employee employee2_3 = new Employee("April", "Aprilson",
    DateTime.Parse("12/12/1992 8:30:52 AM", System.Globalization.CultureInfo.InvariantCulture),
    DateTime.Parse("7/7/2023 8:30:52 AM", System.Globalization.CultureInfo.InvariantCulture),
    49.99,
    Currency.USD,
    sector2);

Supervisor supervisor2 = new Supervisor("Michael", "Michaelson",
    DateTime.Parse("12/12/1975 8:30:52 AM", System.Globalization.CultureInfo.InvariantCulture),
    DateTime.Parse("9/4/2019 8:30:52 AM", System.Globalization.CultureInfo.InvariantCulture),
    139.99,
    Currency.USD,
    sector2,
    "michael.michaelson@gmail.com");

sector2.Employees.Add(employee2_1);
sector2.Employees.Add(employee2_2);
sector2.Employees.Add(employee2_3);
sector2.Supervisor = supervisor2;

//HR SECTOR
Employee employee3_1 = new Employee("Trevor", "Trevorson",
    DateTime.Parse("12/12/1992 8:30:52 AM", System.Globalization.CultureInfo.InvariantCulture),
    DateTime.Parse("7/7/2023 8:30:52 AM", System.Globalization.CultureInfo.InvariantCulture),
    29.99,
    Currency.USD,
    sector2);

Supervisor supervisor3 = new Supervisor("Henry", "Henryson",
    DateTime.Parse("12/12/1975 8:30:52 AM", System.Globalization.CultureInfo.InvariantCulture),
    DateTime.Parse("9/4/2019 8:30:52 AM", System.Globalization.CultureInfo.InvariantCulture),
    69.99,
    Currency.EUR,
    sector3,
    "trevor_trevorson@gmail.com");

sector3.Employees.Add(employee3_1);
sector3.Supervisor = supervisor3;

//INVENTORY & WAREHOUSE

var item6 = new Item("lenovo laptop");
var item7 = new Item("mouse");
var item8 = new Item("keyboard");
var item9 = new Item("standing desk");
var item10 = new Item("whiteboard marker");

firm.Inventory = new Dictionary<Item, Employee>()
{
    { item6, employee1_1 },
    { item7, employee1_1 },
    { item8, employee1_3 },
    { item9, employee2_3 },
    { item10, supervisor1 }
};

var itemA = new Item("hdmi cable");
var itemB = new Item("headphones");
var itemC = new Item("wireless mouse");
var itemD = new Item("charger");

firm.Warehouse = new List<Item>()
{
    itemA, itemB, itemC, itemD
};

//---
StringBuilder stringBuilder = new StringBuilder();
stringBuilder.AppendLine("Enter a number");
stringBuilder.AppendLine("1. Change the pay per hour");
stringBuilder.AppendLine("2. Transfer an employee to another sector");
stringBuilder.AppendLine("3. Setting an employee's working hours & seeing their salary with bonus");
stringBuilder.AppendLine("4. View all employees & newcomers by a supervisor");
stringBuilder.AppendLine("5. Borrow items from supervisor");
stringBuilder.AppendLine("6. Employee requests sick days");
stringBuilder.AppendLine("7. Employee resignation");
stringBuilder.AppendLine("8. Bonus: Inventory & Warehouse in Firm");

Console.Write(stringBuilder);

int choice = Convert.ToInt32(Console.ReadLine());

switch (choice)
{
    case 1:
        Console.WriteLine("\n**********************************");
        Console.WriteLine("Displaying information for the firm after initializing data\n");
        firm.DisplayFirmInformation();

        Console.WriteLine("\n**********************************");
        Console.WriteLine("Trying to change the pay per hour of an employee\n");
        supervisor1.SetEmployeePayPerHour(employee1_1,200.99);

        Console.WriteLine("\n**********************************");
        Console.WriteLine("Trying to change the pay per hour of an employee by a DIFFERENT supervisor\n");
        supervisor1.SetEmployeePayPerHour(employee2_1,300.99);

        Console.WriteLine("\n**********************************");
        Console.WriteLine("Displaying information for the firm after changing pay per hour\n");
        firm.DisplayFirmInformation();

        break;
    
    case 2:
        Console.WriteLine("\n**********************************");
        Console.WriteLine("Displaying information for the firm after initializing data\n");
        firm.DisplayFirmInformation();
        
        Console.WriteLine("\n**********************************");
        Console.WriteLine("Trying to transfer employee to a different sector\n");
        employee1_1.TransferToAnotherSector(sector3);
        
        Console.WriteLine("\n**********************************");
        Console.WriteLine("Displaying information for the firm after transferring employee\n");
        firm.DisplayFirmInformation();
        break;
    
    case 3:
        Console.WriteLine("\n**********************************");
        Console.WriteLine("Displaying information for the firm after initializing data\n");
        firm.DisplayFirmInformation();
        
        Console.WriteLine("\n**********************************");
        Console.WriteLine("Trying to set an employee's hours");
        supervisor1.SetEmployeeHours(employee1_1, 40);
        supervisor1.SetEmployeeHours(employee1_2, 10);
        
        Console.WriteLine("\n**********************************");
        Console.WriteLine("Trying to set the hours of a newcomer");
        supervisor1.SetEmployeeHours(employee1_3, 50);
        
        Console.WriteLine("\n**********************************");
        Console.WriteLine("Trying to set the hours of an employee from a different sector");
        supervisor1.SetEmployeeHours(employee2_1, 30);
        
        Console.WriteLine("\n**********************************");
        Console.WriteLine("Displaying information for the firm after changing hours\n");
        firm.DisplayFirmInformation();

        break;
    
    case 4:
        Console.WriteLine("\n**********************************");
        Console.WriteLine("Displaying information for the firm after initializing data\n");
        firm.DisplayFirmInformation();
        
        Console.WriteLine("\n**********************************");
        Console.WriteLine("Displaying information for all employees & newcomers under a supervisor\n");
        supervisor1.DisplayAllEmployees();
        supervisor1.DisplayAllNewcomers();

        break;
    
    case 5:
        Console.WriteLine("\n**********************************");
        Console.WriteLine("Displaying information for the firm after initializing data\n");
        firm.DisplayFirmInformation();
        
        Console.WriteLine("\n**********************************");
        Console.WriteLine("Trying to borrow an item from the supervisor\n");
        employee1_1.BorrowItemFromSupervisor(supervisor1, item4);
        
        Console.WriteLine("\n**********************************");
        Console.WriteLine("Trying to borrow an item from a supervisor that IS NOT theirs\n");
        employee2_1.BorrowItemFromSupervisor(supervisor1, item5);
        
        Console.WriteLine("\n**********************************");
        Console.WriteLine("Trying to lend an item to an employee\n");
        supervisor1.LendItemToEmployee(employee1_2, item5);
        
        Console.WriteLine("\n**********************************");
        Console.WriteLine("Displaying information for the firm after borrowing and lending items.\n");
        firm.DisplayFirmInformation();

        break;
    
    case 6:
        Console.WriteLine("\n**********************************");
        Console.WriteLine("Displaying information for the firm after initializing data\n");
        firm.DisplayFirmInformation();
        
        Console.WriteLine("\n**********************************");
        Console.WriteLine("Requesting sick day from supervisor\n");
        employee1_1.RequestSickDaysFromSupervisor(supervisor1, new List<DateOnly>()
        {
            DateOnly.FromDateTime(DateTime.Now)
        });
        
        Console.WriteLine("\n**********************************");
        Console.WriteLine("Requesting sick day from supervisor that ISNT theirs\n");
        employee1_1.RequestSickDaysFromSupervisor(supervisor2, new List<DateOnly>()
        {
            DateOnly.FromDateTime(DateTime.Now)
        });
        
        Console.WriteLine("\n**********************************");
        Console.WriteLine("Supervisor requesting sick days for themselves\n");
        supervisor1.RequestSickDaysFromSupervisor(supervisor2, new List<DateOnly>()
        {
            DateOnly.FromDateTime(DateTime.Now)
        });
        
        Console.WriteLine("\n**********************************");
        Console.WriteLine("Displaying information for the firm after requesting sick days\n");
        firm.DisplayFirmInformation();
        break;
    
    case 7:
        
        Console.WriteLine("\n**********************************");
        Console.WriteLine("Displaying information for the firm after initializing data\n");
        firm.DisplayFirmInformation();
        
        Console.WriteLine("\n**********************************");
        Console.WriteLine("Newcomer tries to resign:\n");
        employee1_3.Resign();
        
        Console.WriteLine("\n**********************************");
        Console.WriteLine("Employee tries to resign:\n");
        employee1_1.Resign();
        
        Console.WriteLine("\n**********************************");
        Console.WriteLine("Displaying information for the firm after employee resignation\n");
        firm.DisplayFirmInformation();
        
        break;
    
    case 8:
        
        Console.WriteLine("\n**********************************");
        Console.WriteLine("Displaying information for the firm items after initializing data\n");
        firm.DisplayItemsBeingUsed();
        firm.DisplayItemsInWarehouse();
        
        Console.WriteLine("\n**********************************");
        Console.WriteLine("Trying to borrow an item that is being used by another employee");
        firm.BorrowItem(employee2_3,item6);
        Console.WriteLine("Trying to borrow an item that is in your supervisor's inventory");
        firm.BorrowItem(employee1_3,item10);
        Console.WriteLine("Trying to borrow an item that is in the warehouse (isn't being used by anyone)");
        firm.BorrowItem(employee1_1,itemA);
        Console.WriteLine("Trying to borrow an item that isn't in the firm");
        firm.BorrowItem(employee2_1,new Item("TV"));
        
        Console.WriteLine("\n**********************************");
        Console.WriteLine("Trying to return an item that is being used by an employee");
        firm.ReturnItem(employee1_1,item7);
        Console.WriteLine("Trying to return an item being used by someone else");
        firm.ReturnItem(employee1_1,item10);
        Console.WriteLine("Trying to return an item that is already in the warehouse");
        firm.ReturnItem(employee1_2,itemB);
        Console.WriteLine("Trying to return an item that isn't in the firm");
        firm.ReturnItem(employee2_1,new Item("Playstation"));
        
        
        Console.WriteLine("\n**********************************");
        Console.WriteLine("Displaying information for the firm items after borrowing & returning items.\n");
        firm.DisplayItemsBeingUsed();
        firm.DisplayItemsInWarehouse();
        
        break;
    
    default:
        
        Console.WriteLine("Please pick an integer from 1-8");
        break;
        
}
