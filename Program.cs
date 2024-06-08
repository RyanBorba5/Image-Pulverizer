using DataJuggler.PixelDatabase;
using System.Drawing;

namespace DSRTexturePixelRando
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Insert the path to the png you want to pulverize below...");
            Console.WriteLine("Make sure it's not in quotes. Example: C:\\Users\\Hungr\\Desktop\\pixelmessing\\Bunny.png");
            String imagepath = Console.ReadLine();
            PixelRandomizer(imagepath);
        }

        public static void PixelRandomizer(string imagePath)
        {
            string tempFilePath = Path.Combine(Path.GetDirectoryName(imagePath), "Temporary_File.png");
                Bitmap mybitmap;
                using (Image imagetorandomize = Image.FromFile(imagePath))
                {
                    mybitmap = new Bitmap(imagetorandomize);
                }

                List<Color> pixels = new List<Color>();
                List<Color> finalpixels = new List<Color>();

                Console.WriteLine("Collecting pixels...");
                // Collect all pixel colors
                for (int y = 0; y < mybitmap.Height; y++)
                {
                    for (int x = 0; x < mybitmap.Width; x++)
                    {
                        pixels.Add(mybitmap.GetPixel(x, y));
                    }
                }

                Console.WriteLine("Randomizing pixels...");
                // Randomize the list of pixels
                Random r = new Random();
                while (pixels.Count > 0)
                {
                    int randomIndex = r.Next(pixels.Count);
                    finalpixels.Add(pixels[randomIndex]);
                    pixels.RemoveAt(randomIndex);
                    Console.WriteLine("Pixels left to pulverize: "+pixels.Count);
                }

                Console.WriteLine("Adding randomized pixels and finishing up...");
                // Apply randomized pixels back to the bitmap
                int k = 0;
                for (int y = 0; y < mybitmap.Height; y++)
                {
                    for (int x = 0; x < mybitmap.Width; x++)
                    {
                        mybitmap.SetPixel(x, y, finalpixels[k]);
                        k++;
                    }
                Console.WriteLine(y + "/" + mybitmap.Height);
                }

                // Save the edited image to a temporary file
                mybitmap.Save(tempFilePath);
                mybitmap.Dispose();

                // Ensure all resources are released before replacing the file
                GC.Collect();
                GC.WaitForPendingFinalizers();

                Console.WriteLine("Replacing Original Image...");
                // Replace the original image with the temporary file
                File.Delete(imagePath);
                File.Move(tempFilePath, imagePath);

                Console.WriteLine("Image replaced successfully.");
        }
        //public static void PixelRandomizer(String imagepath)
        //{
        //    String tempFilePath = @"C:\Users\Hungr\Desktop\pixelmessing\Temporary File.png";
        //    Image imagetorandomize = Image.FromFile(imagepath);
        //    Bitmap mybitmap = new Bitmap(imagetorandomize);
        //    List<Color> pixels = new List<Color>();
        //    List<Color> finalpixels = new List<Color>();
        //    int l = 0;
        //    int k = 0;
        //    Random r = new Random();

        //    using (imagetorandomize = Image.FromFile(imagepath))
        //    using (mybitmap = new Bitmap(imagetorandomize))
        //    {
        //        for (int y = 0; y < imagetorandomize.Height; y++)
        //        {
        //            for (int x = 0; x < imagetorandomize.Width; x++) 
        //            {
        //                l++;
        //                Console.WriteLine("Saving pixel #{0}...",l);
        //                pixels.Add(mybitmap.GetPixel(x, y));
        //            }
        //        }
        //        Console.WriteLine("Pixels added. Now Attempting to randomize list...");
        //        int pixellistsize = pixels.Count;
        //        for (int i = 0; i < pixellistsize; i++)
        //        {
        //            int randomnumber = r.Next(0,pixels.Count);
        //            finalpixels.Add(pixels[randomnumber]);
        //            pixels.Remove(pixels[randomnumber]);
        //        }

        //        Console.WriteLine("List randomized. Now attempting to replace pixels...");

        //        for (int y = 0; y < imagetorandomize.Height; y++)
        //        {
        //            for (int x = 0; x < imagetorandomize.Width; x++)
        //            {
        //                Console.WriteLine("Pixel at x: " + x + " and y: " + y + "has been changed...");
        //                mybitmap.SetPixel(x, y, finalpixels[k]);
        //                k++;
        //            }
        //        }
        //        mybitmap.Save(tempFilePath);
        //        mybitmap.Dispose();

        //    }

        //    GC.Collect();
        //    GC.WaitForPendingFinalizers();

        //    File.Delete(imagepath);
        //    File.Move(tempFilePath, imagepath);
        //}
                
    }
}
