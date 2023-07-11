using System.Collections.Concurrent;
using System.Text;

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

    private async Task SetBrightnessGroup(ConcurrentBag<Image> imagesGroup)
    {
        foreach (var image in imagesGroup)
        {
            await Task.Delay(2000);
            switch (image.Filter)
            {
                case Filter.SEPIA:
                    image.Brightness = -5;
                    break;
                case Filter.BLUR:
                    image.Brightness = 0;
                    break;
                case Filter.GRAYSCALE:
                    image.Brightness = 10;
                    break;
            }
            Console.WriteLine($"The brightness of image {image.Name} has been set to {image.Brightness}.");
        }
    }

    public async Task SetBrightnessAll()
    {
        ConcurrentBag<Image> sepiaImages = new ConcurrentBag<Image>();
        ConcurrentBag<Image> blurImages = new ConcurrentBag<Image>();
        ConcurrentBag<Image> grayscaleImages = new ConcurrentBag<Image>();

        Parallel.ForEach(Images,
            new ParallelOptions { MaxDegreeOfParallelism = 10 },
            (image) =>
            {
                switch (image.Filter)
                {
                    case Filter.SEPIA:
                        sepiaImages.Add(image);
                        break;
                    case Filter.BLUR:
                        blurImages.Add(image);
                        break;
                    case Filter.GRAYSCALE:
                        grayscaleImages.Add(image);
                        break;
                }
            });
        
        //printing info an all the groups
        Console.WriteLine($"Sepia images: ( {DisplayGroup(sepiaImages)})");
        Console.WriteLine($"Blur images: ( {DisplayGroup(blurImages)})");
        Console.WriteLine($"Grayscale images: ( {DisplayGroup(grayscaleImages)})");

        var tasks = new List<Task>();
        //The groups can run at the same time:
        tasks.Add(SetBrightnessGroup(sepiaImages));
        tasks.Add(SetBrightnessGroup(blurImages));
        tasks.Add(SetBrightnessGroup(grayscaleImages));
        await Task.WhenAll(tasks);
        
        Console.WriteLine("The entire task is complete");
        
    }

    private string DisplayGroup(ConcurrentBag<Image> images)
    {
        var stringBuilder = new StringBuilder();
        foreach (var image in images)
        {
            stringBuilder.Append($"{image.Name} ");
        }
        return stringBuilder.ToString();
    }

}