using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepLearningNumberRecognition
{
    /// <summary>
    /// MNIST image converter from byte[][] to int[][].
    /// </summary>
    public class MNISTReader
    {
        private string path = Path.GetFullPath($@"..\..\MNIST DataBase\");

        public int[][][] TestImagesArray { get; private set; }
        public int[][][] TrainingImagesArray { get; private set; }
        public int[] TestImagesLabel { get; private set; }
        public int[] TrainingImagesLabel { get; private set; }

        /// <summary>
        /// Set up jagged array for 60 000 training images and 10 000 test images with 28x28 size.
        /// </summary>
        public MNISTReader()
        {
            int mnistLength = 60000; //60 000 for training images

            TrainingImagesLabel = new int[mnistLength]; //prepare training labbel array
            TrainingImagesArray = new int[mnistLength][][]; //prepare traning images array

            for (int i = 0; i < mnistLength; ++i)
            {
                TrainingImagesArray[i] = new int[28][];

                for (int j = 0; j < 28; j++)
                {
                    TrainingImagesArray[i][j] = new int[28];
                }

            }

            mnistLength = 10000; //10 000 test images
            TestImagesLabel = new int[mnistLength];
            TestImagesArray = new int[mnistLength][][];

            for (int i = 0; i < mnistLength; ++i)
            {
                TestImagesArray[i] = new int[28][];

                for (int j = 0; j < 28; j++)
                {
                    TestImagesArray[i][j] = new int[28];
                }

            }

            SetTestImagesArray();
            SetTestImagesLabels();
            SetTrainingImagesArray();
            SetTrainingImagesLabels();       
        }

        /// <summary>
        /// Set all test images from MNIST [img number][pixel][pixel].
        /// </summary>
        /// <returns></returns>
        private void SetTestImagesArray()
        {
            SetImagesArray(TestImagesArray, "t10k-images.idx3-ubyte");
        }

        /// <summary>
        /// Set int[] of test images labels.
        /// </summary>
        /// <returns></returns>
        private void SetTestImagesLabels()
        {
            SetLabelsArray(TestImagesLabel, "t10k-labels.idx1-ubyte");
        }

        /// <summary>
        /// Set all training images from MNIST [img number][pixel][pixel].
        /// </summary>
        /// <returns></returns>
        private void SetTrainingImagesArray()
        {
            SetImagesArray(TrainingImagesArray, "train-images.idx3-ubyte");
        }

        /// <summary>
        /// Set int[] of training images labels.
        /// </summary>
        /// <returns></returns>
        private void SetTrainingImagesLabels()
        {
            SetLabelsArray(TrainingImagesLabel, "train-labels.idx1-ubyte");
        }

        private void SetImagesArray(int[][][] imageArray, string imageFileName)
        {
            var ifsImages = new FileStream($@"{path}{imageFileName}", FileMode.Open); // test images
            var brImages = new BinaryReader(ifsImages);

            int magic1 = brImages.ReadInt32(); // discard
            int numImages = brImages.ReadInt32();
            int numRows = brImages.ReadInt32();
            int numCols = brImages.ReadInt32();

            for (int k = 0; k < imageArray.Length; k++)
            {
                for (int i = 0; i < 28; i++)
                {
                    for (int j = 0; j < 28; j++)
                    {
                        byte b = brImages.ReadByte();
                        imageArray[k][i][j] = b;
                    }
                }
            }

        }

        private void SetLabelsArray(int[] labelArray, string labelFileName)
        {
            var ifsLabels = new FileStream($@"{path}{labelFileName}", FileMode.Open); // test labels
            var brLabels = new BinaryReader(ifsLabels);

            int magic2 = brLabels.ReadInt32();
            int numLabels = brLabels.ReadInt32();
            byte label;

            for (int i = 0; i < labelArray.Length; i++)
            {
                label = brLabels.ReadByte();
                labelArray[i] = Convert.ToInt32(label);
            }

        }

        /// <summary>
        /// Write MNIST images at console window use bool singleSelect for show image by image
        /// </summary>
        /// <param name="image"></param>
        /// <param name="label"></param>
        /// <param name="singleSelect"></param>
        public void ConsolePrintImage(int[][][] image, int[] label, bool singleSelect = false)
        {
            

            for(int k = 0; k < image.GetLength(0); ++k)
            {
                var writer = new StringBuilder(28 * 28 *2);
                
                for(int i = 0; i < 28; ++i)
                {
                    for(int j=0;j < 28; ++j)
                    {
                        if(image[k][i][j] == 0)
                        {
                            writer.Append(" ");//white
                        }
                        else if(image[k][i][j] == 255) //black
                        {
                            writer.Append("0");
                        }
                        else
                        {
                            writer.Append("."); //grey
                        }

                      
                    }
                    writer.Append("\n");
                }
                writer.Append($"Current value: {label[k]} \nProgress:{k + 1}/{image.GetLength(0)}");
                Console.WriteLine(writer);
                if (singleSelect)
                {
                    Console.ReadKey();
                }
                
            }
        }

    }

}

