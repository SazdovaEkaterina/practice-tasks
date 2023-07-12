namespace Task2AsyncExercise0;

public class Program
{
    public static async Task Main()
    {
        string inputFilePath = "/Users/ekaterinasazdova/Documents/GitHub/practice-tasks/DataProcessingApplication/input.txt";
        string outputFilePath = "/Users/ekaterinasazdova/Documents/GitHub/practice-tasks/DataProcessingApplication/output.txt";

        DataProcessor dataProcessor = new DataProcessor(inputFilePath, outputFilePath);
        await dataProcessor.ReadFile();
        
    }
}