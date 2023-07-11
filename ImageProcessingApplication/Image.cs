namespace ImageProcessingApplication;

public class Image
{
    public string Name { get; set; }
    public Filter Filter { get; set; }
    public int Brightness { get; set; }

    public Image(string name)
    {
        Name = name;
        Filter = Filter.NONE;
        Brightness = 0;
    }
}