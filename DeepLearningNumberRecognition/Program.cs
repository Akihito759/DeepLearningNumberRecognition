using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DeepLearningNumberRecognition.Adapter;
using System.Diagnostics;

namespace DeepLearningNumberRecognition
{
    class Program
    {
        static void Main(string[] args)
        {
            var sw = new Stopwatch();
            sw.Start();
            var reader = new MNISTReader();
            var trainingAdapeter = new MistToNeutralNetworkAdapter(reader.TrainingImagesArray, reader.TrainingImagesLabel, 1000);
            var testAdapter = new MistToNeutralNetworkAdapter(reader.TestImagesArray, reader.TestImagesLabel, 100);
            var DDP = new NeutralNetwork();
            DDP.TrainNetwork(trainingAdapeter.ImagesVectorArray, trainingAdapeter.LabelsArray);
            DDP.ComputeNetwork(testAdapter.ImagesVectorArray, testAdapter.LabelsArray);
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
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