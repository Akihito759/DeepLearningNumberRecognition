using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DeepLearningNumberRecognition.Adapter;

namespace DeepLearningNumberRecognition
{
    class Program
    {
        static void Main(string[] args)
        {

            var reader = new MNISTReader();
            var adapter = new MistToNeutralNetworkAdapter(reader.TestImagesArray, reader.TestImagesLabel);
            var test = new NeutralNetwork(adapter.ImagesVectorArray, adapter.LabelsArray);
            Console.ReadKey();
            //try
            //{
            //    var test = new DigitImage();
            //    test.ShowTestImages(true);
            //}

            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    Console.ReadLine();
            //}
        }



    } 
}