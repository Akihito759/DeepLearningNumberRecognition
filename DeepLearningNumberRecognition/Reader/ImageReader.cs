using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace DeepLearningNumberRecognition.Reader
{
    public class ImageReader
    {
        private string _path = Path.GetFullPath($@"..\..\Images\");
        public double[][] ImagesArray { get; private set; }


        /// <summary>
        /// Load all images from \Images\ and convert to int[][] array.
        /// </summary>
        public ImageReader()
        {
            var imagesPathList = Directory.GetFiles(_path, "*.*", SearchOption.AllDirectories).ToList();
            var imagesBitmapList = new List<Bitmap>();

            ImagesArray = new double[imagesPathList.Count][];
            int count = 0;
            foreach (var item in imagesPathList)
            {
                try
                {
                    Console.WriteLine(item);
                    Image image = Image.FromFile(item); //load image
                    Bitmap bitmap = new Bitmap(image); //create bitmap
                    ImagesArray[count++] = ChangeToGrayScale(ref bitmap);// add grey scale

                }
                catch
                {

                }
            }



        }

        private double[] ChangeToGrayScale(ref Bitmap image)
        {
            double[] array = new double[28 * 28];
            int z = 0;
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    Color c = image.GetPixel(j, i);

                    int r = c.R;
                    int g = c.G;
                    int b = c.B;
                    int avg = (r + g + b) / 3;
                    
                    array[z++] = 255 - avg; //Make negative        
                    image.SetPixel(j, i, Color.FromArgb(avg, avg, avg));
                }
            }
            //string txt = null;
            //for (int i = 0; i < 28 * 28; i++)
            //{
            //    if (array[i] == 0)
            //    {
            //        txt += " ";
            //    }
            //    else if (array[i] == 255)
            //    {
            //        txt += "0";
            //    }
            //    else
            //    {
            //        txt += ".";
            //    }
            //    if (i % 28 == 0)
            //    {
            //        txt += "\n";
            //    }

            //}
            //Console.WriteLine(txt);

            return array;
        }
    }
}
