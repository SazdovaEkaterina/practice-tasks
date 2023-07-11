using System.Collections.Concurrent;

namespace ImageProcessingApplication;

public class ImageProcessor
{
    public List<Image> Images { get; set; }

    public ImageProcessor(List<Image> images)
    {
        Images = images;
    }

    private void ApplyFilter(Image image)
    {
        var random = new Random();
        var filters = new List<Filter> { Filter.SEPIA, Filter.GRAYSCALE, Filter.BLUR };
        var filter = filters[random.Next(filters.Count)];
        image.Filter = filter;
        Console.WriteLine($"The filter {filter} has been applied to the image {image.Name}");
    }

    public void ApplyAllFilters()
    {
        //Applying of the filters should happen simultaneously 
        Parallel.ForEach(Images,
            new ParallelOptions {MaxDegreeOfParallelism = 10},
            (image) => ApplyFilter(image));
        
        //Once all images have filters applied you should print out which filter the image has. 
        foreach (var image in Images)
        {
            Console.WriteLine($"The image {image.Name} has the filter {image.Filter}.");
        }
    }

    public async Task SetBrightness()
    {
        
    }

}