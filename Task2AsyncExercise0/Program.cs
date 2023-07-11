namespace Task2AsyncExercise0;

public class Program
{
    public static async Task Main()
    {
        string inputFilePath = "/Users/ekaterinasazdova/Documents/GitHub/practice-tasks/Task2AsyncExercise0/input.txt";
        string outputFilePath = "/Users/ekaterinasazdova/Documents/GitHub/practice-tasks/Task2AsyncExercise0/output.txt";

        DataProcessor dataProcessor = new DataProcessor(inputFilePath, outputFilePath);
        await dataProcessor.ReadFile();
        
    }
}