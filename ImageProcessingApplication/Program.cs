using ImageProcessingApplication;

//LIST OF 10 IMAGES
List<Image> images = new List<Image>()
{
    new Image("flowers.jpg"),
    new Image("cats.png"),
    new Image("dogs.jpeg"),
    new Image("clouds.gif"),
    new Image("nature.jpg"),
    new Image("trees.jpg"),
    new Image("sunflowers.png"),
    new Image("birds.gif"),
    new Image("roses.png"),
    new Image("lake.jpeg")
};

ImageProcessor imageProcessor = new ImageProcessor(images);

//APPLY RANDOM FILTER (ALL IMAGES AT THE SAME TIME)
//PRINT IMAGE + FILTER ONCE APPLIED
imageProcessor.ApplyAllFilters();

//ONCE FILTERS ARE DONE, SET BRIGHTNESS FOR EACH GROUP SIMULTANEOUSLY, BUT SEQUENTIALLY WITHIN THE GROUP
//AFTER EVERYTHING IS DONE PRINT A MESSAGE INDICATING COMPLETION
await imageProcessor.SetBrightnessAll();
