using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;

namespace DSPLabs;

public class CustomPainter
{
    public static WriteableBitmap? WindowLoaded(int width, int height, byte[,,] pixels)
    {
        if (pixels.GetLength(2) != 4)
            return null;

        WriteableBitmap wbitmap = new WriteableBitmap(
            width, height, 96, 96, PixelFormats.Bgra32, null);



        // Copy the data into a one-dimensional array.
        byte[] pixels1d = new byte[height * width * 4];
        int index = 0;
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                for (int i = 0; i < 4; i++)
                    pixels1d[index++] = pixels[row, col, i];
            }
        }

        // Update writeable bitmap with the colorArray to the image.
        Int32Rect rect = new Int32Rect(0, 0, width, height);
        int stride = 4 * width;
        wbitmap.WritePixels(rect, pixels1d, stride, 0);
        return wbitmap;
    }
}
