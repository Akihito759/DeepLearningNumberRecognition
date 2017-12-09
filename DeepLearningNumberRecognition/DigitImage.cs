using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepLearningNumberRecognition
{
    public class DigitImage
    {
       
        private string path = Path.GetFullPath($@"..\..\MNIST DataBase\");

        /// <summary>
        /// Console write all TEST images. Use bool parametr to present one by one.
        /// </summary>
        /// <param name="singleSelect"></param>
        public void ShowTestImages(bool singleSelect = false)
        {
            Console.WriteLine("\n Begin test images\n");
            GenerateImages("t10k-labels.idx1-ubyte", "t10k-images.idx3-ubyte", 10000, singleSelect);
            Console.ReadLine();
        }

        /// <summary>
        /// Console write all TRAINING images. Use bool parametr to present one by one.
        /// </summary>
        /// <param name="singleSelect"></param>
        public void ShowTrainingImages(bool singleSelect = false)
        {
            Console.WriteLine("\n Begin Training images\n");
            GenerateImages("train-labels.idx1-ubyte","train-images.idx3-ubyte",60000, singleSelect);
            Console.ReadLine();
        }

        /// <summary>
        /// Get int[] array of TestImagesLabels.
        /// </summary>
        /// <returns></returns>
        public int[] GetTestImagesLabels()
        {
            var labelsArray = new int[10000];
            var ifsLabels = new FileStream($@"{path}t10k-labels.idx1-ubyte", FileMode.Open); // test labels
            var brLabels = new BinaryReader(ifsLabels);

            int magic2 = brLabels.ReadInt32();
            int numLabels = brLabels.ReadInt32();
            byte label;

            for (int i = 0; i < labelsArray.Length; i++)
            {
                label = brLabels.ReadByte();
                labelsArray[i] = Convert.ToInt32(label);
            }
            return labelsArray;
        }

        /// <summary>
        /// Get int[] array of TrainingImagesLabels.
        /// </summary>
        /// <returns></returns>
        public int[] GetTrainingImagesLabels()
        {
            var labelsArray = new int[60000];
            var ifsLabels = new FileStream($@"{path}train-labels.idx1-ubyte", FileMode.Open); // test labels
            var brLabels = new BinaryReader(ifsLabels);

            int magic2 = brLabels.ReadInt32();
            int numLabels = brLabels.ReadInt32();
            byte label;

            for (int i = 0; i < labelsArray.Length; i++)
            {
                label = brLabels.ReadByte();
                labelsArray[i] = Convert.ToInt32(label);
            }
            return labelsArray;
        }


        private void GenerateImages(string labelPath, string imagePath, int maxDi, bool singleSelect = false)
        {

            var ifsLabels = new FileStream($@"{path}{labelPath}", FileMode.Open); // test labels
            var ifsImages = new FileStream($@"{path}{imagePath}", FileMode.Open); // test images

            var brLabels = new BinaryReader(ifsLabels);
            var brImages = new BinaryReader(ifsImages);

            int magic1 = brImages.ReadInt32(); // discard
            int numImages = brImages.ReadInt32();
            int numRows = brImages.ReadInt32();
            int numCols = brImages.ReadInt32();

            int magic2 = brLabels.ReadInt32();
            int numLabels = brLabels.ReadInt32();

            byte[][] pixels = new byte[28][];
            for (int i = 0; i < pixels.Length; ++i)
            {
                pixels[i] = new byte[28];
            }

            // each test image
            for (int di = 0; di < maxDi; ++di)
            {
                for (int i = 0; i < 28; ++i)
                {
                    for (int j = 0; j < 28; ++j)
                    {
                        byte b = brImages.ReadByte();
                        pixels[i][j] = b;
                    }
                }

                byte label = brLabels.ReadByte();

                PrintImage(pixels, label, di, maxDi);
                if (singleSelect)
                {
                    Console.ReadLine();
                }
            } // each image

            ifsImages.Close();
            brImages.Close();
            ifsLabels.Close();
            brLabels.Close();
        }

        private void PrintImage(byte[][] pixels, byte label, int index, int maxIndex)
        {
            string s = "";
            for (int i = 0; i < 28; ++i)
            {
                for (int j = 0; j < 28; ++j)
                {
                    if (pixels[i][j] == 0)
                        s += " "; // white
                    else if (pixels[i][j] == 255)
                        s += "O"; // black
                    else
                        s += "."; // gray
                }
                s += "\n";
            }
            s += $"Current value: {label.ToString()} \nProgress:{index + 1}/{maxIndex}";
            Console.WriteLine(s);
        }
    }

}
