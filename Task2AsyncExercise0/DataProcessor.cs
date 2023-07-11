using System.Text;
namespace Task2AsyncExercise0;

public class DataProcessor
{
    public string InputFilePath { get; set; }
    public string OutputFilePath { get; set; }
    public string Stats { get; set; }
    public string ProcessedData { get; set; }
    public DataProcessor(string inputFilePath, string outputFilePath)
    {
        InputFilePath = inputFilePath;
        OutputFilePath = outputFilePath;
    }

    //Task 1: Data Ingestion 
    public async Task ReadFile()
    {
        Console.WriteLine("Task 1: Data Ingestion -> Running...");
        string[] lines = await File.ReadAllLinesAsync(InputFilePath);
        await Task.Delay(5000); //This task takes around 5 seconds to complete
        Console.WriteLine("Task 1: Data Ingestion -> Finished");
        
        await PrepareData(lines); //Task 2 can be started immediately after Task 1 completes. 
    }
    
    //Task 2: Data Preprocessing
    public async Task PrepareData(string[] lines)
    {
        Console.WriteLine("Task 2: Data Preprocessing -> Running...");
        await Task.Delay(10000); //This task prepares the data (around 10 sec)
        
        List<Product> products = new List<Product>();
        foreach (var line in lines)
        {
            string[] items = line.Split(" ");
            Product product = new Product(items[0], Double.Parse(items[1]), Int32.Parse(items[2]));
            products.Add(product);
        }
        Console.WriteLine("Task 2: Data Preprocessing -> Finished");
        
        //Task 3 can run concurrently with Task 4. 
        var tasks = new List<Task>();
        tasks.Add(ProcessData(products)); //Task 3
        tasks.Add(StatisticalAnalysis(products)); //Task 4

        await Task.WhenAll(tasks); //Tasks 3 & 4 run in parallel
        
        await WriteToFile(); //Task 5 runs after 4 & 4 are finished
    }
    
    //Task 3: Parallel Data Processing 
    public async Task ProcessData(List<Product> products)
    {
        Console.WriteLine("Task 3: Parallel Data Processing -> Running...");
        await Task.Delay(5000);
        
        StringBuilder stringBuilder = new StringBuilder();
        
        double totalProfit = 0;
        int itemsOutOfStock = 0;

        foreach (var product in products)
        {
            totalProfit += product.Price * product.Stock;
            if (product.Stock == 0)
            {
                itemsOutOfStock++;
            }
        }

        stringBuilder.AppendLine($"Total profit that can be made from all items & stock: ${totalProfit:000.000.00}");
        stringBuilder.AppendLine($"Number of items that are out of stock: {itemsOutOfStock}");

        ProcessedData = stringBuilder.ToString();
        
        Console.WriteLine("Task 3: Parallel Data Processing -> Finished");
    }

    //Task 4: Data Analysis
    public async Task StatisticalAnalysis(List<Product> products)
    {
        Console.WriteLine("Task 4: Data Analysis -> Running...");
        await Task.Delay(5000);
        
        StringBuilder stringBuilder = new StringBuilder();
        
        double minItemPrice = products.MinBy(product => product.Price).Price;
        double maxItemPrice = products.MaxBy(product => product.Price).Price;

        stringBuilder.AppendLine($"Min item price: ${minItemPrice}");
        stringBuilder.AppendLine($"Max item price: ${maxItemPrice}");

        double averageItemPrice = 0;
        foreach (var product in products)
        {
            averageItemPrice += product.Price;
        }
        averageItemPrice = averageItemPrice / products.Count;
        stringBuilder.AppendLine($"Average item price: ${averageItemPrice:0.00}");
        
        Stats = stringBuilder.ToString();
        
        Console.WriteLine("Task 4: Data Analysis -> Finished");
    }

    //Task 5: Data Output 
    public async Task WriteToFile()
    {
        Console.WriteLine("Task 5: Data Output -> Running...");
        await Task.Delay(3000);
        
        StreamWriter writer = new StreamWriter(OutputFilePath);

        await writer.WriteLineAsync("***---Processed Data---***");
        await writer.WriteAsync(ProcessedData);
        
        await writer.WriteLineAsync("\n***---Price Statistics---***");
        await writer.WriteAsync(Stats);
        
        writer.Close();
        
        Console.WriteLine("Task 5: Data Output -> Finished");
    }

}