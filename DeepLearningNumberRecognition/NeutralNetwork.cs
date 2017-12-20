using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.MachineLearning;
using Accord.IO;
using Accord.Statistics;
using Accord.Neuro;
using Accord.Neuro.Learning;
using Accord.Neuro.Networks;

namespace DeepLearningNumberRecognition
{
    public class NeutralNetwork
    {
        private DeepBeliefNetwork _network;
        private DeepNeuralNetworkLearning _teacher;

        public NeutralNetwork(int inputNeuronsCount, int[] hiddenNeurons)
        {
            _network = new DeepBeliefNetwork(inputNeuronsCount, hiddenNeurons);
            new GaussianWeights(_network).Randomize();
            _network.UpdateVisibleWeights();

            _teacher = new DeepNeuralNetworkLearning(_network)
            {
                Algorithm = (ann, i) => new ParallelResilientBackpropagationLearning(ann),
                LayerIndex = _network.Machines.Count - 1,
            };
        }

        public NeutralNetwork()
        {
            _network = new DeepBeliefNetwork(784, new int[] { 1000, 10 });
            new GaussianWeights(_network).Randomize();
            _network.UpdateVisibleWeights();

            _teacher = new DeepNeuralNetworkLearning(_network)
            {
                Algorithm = (ann, i) => new ParallelResilientBackpropagationLearning(ann),
                LayerIndex = _network.Machines.Count - 1,
            };
        }

        public void TrainNetwork(double[][] input, double[][] output,int epochNumber=5000)
        {
            var layerData = _teacher.GetLayerInput(input);

            for (int i = 0; i < epochNumber; i++)
            {
                _teacher.RunEpoch(layerData, output);
            }
            _network.UpdateVisibleWeights();
        }

        public void ComputeNetwork(double[][] input,double[][] output)
        {
            int err=0;

            for (int i = 0; i < input.Length; i++)
            {
              var networkOutput = _network.Compute(input[i]);
              if(Array.IndexOf(networkOutput, networkOutput.Max()) != Array.IndexOf(output[i], output[i].Max()))
                {
                    err++;
                }
            }
            Console.WriteLine($"Total:{input.Length} \n  Err:{err} \n Err(%) ={((double)err / (double)input.Length)*100}%");
        }



        public NeutralNetwork(double[][] input, double[][] output, double[][] testInput, double[][] testOutput)
        {

            var network = new DeepBeliefNetwork(28 * 28, new int[] { 1000, 10 });

            new GaussianWeights(network).Randomize();
            network.UpdateVisibleWeights();

            var teacher = new DeepNeuralNetworkLearning(network)
            {
                Algorithm = (ann, i) => new ParallelResilientBackpropagationLearning(ann),
                LayerIndex = network.Machines.Count - 1,
            };
            var layerData = teacher.GetLayerInput(input);

            for (int i = 0; i < 5000; i++)
            {
                teacher.RunEpoch(layerData, output);
            }
            network.UpdateVisibleWeights();
            var inputArr = new double[28 * 28];

            for (int i = 0; i < 28 * 28; i++)
            {
                inputArr[i] = testInput[0][i];
            }

            var a = network.Compute(testInput[0]);
            Console.WriteLine(Array.IndexOf(a, a.Max()));


        }
    }
}
