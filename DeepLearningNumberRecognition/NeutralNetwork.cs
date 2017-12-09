using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.MachineLearning;
using Accord.IO;
using Accord.Neuro;
using Accord.Neuro.Learning;

namespace DeepLearningNumberRecognition
{
    public class NeutralNetwork
    {
        public NeutralNetwork()
        {
            IActivationFunction activationFunction = new BipolarSigmoidFunction();

            var network = new ActivationNetwork(activationFunction, 784, new[] { 1000, 10 });

            var teacher = new LevenbergMarquardtLearning(network)
            {
                UseRegularization = true
            };

        }
    }
}
