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

    private async Task ApplyFilter(Image image)
    {
        await Task.Delay(2500);
        var random = new Random();
        var filters = new List<Filter> { Filter.SEPIA, Filter.GRAYSCALE, Filter.BLUR };
        var filter = filters[random.Next(filters.Count)];
        image.Filter = filter;
        Console.WriteLine($"The filter {filter} has been applied to the image {image.Name}");
    }

    public async Task ApplyAllFilters()
    {
        //NACHIN 1
        //Applying of the filters should happen simultaneously 
        // await Parallel.ForEachAsync(Images,
        //     new ParallelOptions { MaxDegreeOfParallelism = 10 },
        //     async (image, _) => await ApplyFilter(image));

        //NACHIN 2
        // var tasks = new List<Task>();
        // foreach (var image in Images)
        // {
        //     tasks.Add(ApplyFilter((image)));
        // }
        //await Task.WhenAll(tasks);
        
        //NACHIN 3
        var tasks = new List<Task>();
        Images.ForEach(image =>
        {
            tasks.Add(ApplyFilter((image)));
        });
        await Task.WhenAll(tasks);

        //Once all images have filters applied you should print out which filter the image has. 
        foreach (var image in Images)
        {
            Console.WriteLine($"The image {image.Name} has the filter {image.Filter}.");
        }
    }

    private async Task SetBrightnessGroup(List<Image> imagesGroup)
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
        List<Image> sepiaImages = new List<Image>();
        List<Image> blurImages = new List<Image>();
        List<Image> grayscaleImages = new List<Image>();

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

    private string DisplayGroup(List<Image> images)
    {
        var stringBuilder = new StringBuilder();
        foreach (var image in images)
        {
            stringBuilder.Append($"{image.Name} ");
        }
        return stringBuilder.ToString();
    }

}