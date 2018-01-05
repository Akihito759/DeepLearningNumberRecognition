using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DeepLearningNumberRecognition.Reader;
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
            //reader.ConsolePrintImage(reader.TestImagesArray, reader.TestImagesLabel, true); //Wypisywanie obrazków 
            var trainingAdapeter = new MistToNeutralNetworkAdapter(reader.TrainingImagesArray, reader.TrainingImagesLabel, 1000);
            var testAdapter = new MistToNeutralNetworkAdapter(reader.TestImagesArray, reader.TestImagesLabel, 10000);
            var DDP = new NeutralNetwork();
            DDP.LoadNetwork("Network60k1k"); //wczytranie przetrenowanej sieci neuronowej
            var ImageReaader = new ImageReader();
            //DDP.TrainNetwork(trainingAdapeter.ImagesVectorArray, trainingAdapeter.LabelsArray); funkcja do terenowania sieci neuronwoej 
            DDP.ComputeNetwork(testAdapter.ImagesVectorArray, testAdapter.LabelsArray);
            DDP.ComputeNetwork(ImageReaader.ImagesArray);
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