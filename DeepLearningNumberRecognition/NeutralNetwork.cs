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

namespace DeepLearningNumberRecognition
{
    public class NeutralNetwork
    {
        public NeutralNetwork(double[][] input,double[][] output)
        {
            IActivationFunction activationFunction = new BipolarSigmoidFunction();

            var network = new ActivationNetwork(activationFunction, 784, new[] { 100, 10 });

            var teacher = new LevenbergMarquardtLearning(network)
            {
                UseRegularization = true
            };

            var error = teacher.RunEpoch(input, output);
            Console.WriteLine(error);

        }
    }
}
