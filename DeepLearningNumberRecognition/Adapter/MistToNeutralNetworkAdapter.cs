using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepLearningNumberRecognition.Adapter
{
    public class MistToNeutralNetworkAdapter
    {
        public double[][] ImagesVectorArray { get; private set; }
        public double[][] LabelsArray { get; private set; }

        public MistToNeutralNetworkAdapter(int[][][] array,int[] label)
        {
            if (array.GetLength(0) == label.Length)
            {
                ImagesVectorArray = GetImagesVectorsArray(array);
                LabelsArray = GetLebelsArray(label);
            }
        }

        /// <summary>
        /// Get ImageVectorsArray for DeepLearning module.
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public double[][] GetImagesVectorsArray(int[][][] array)
        {
            var VectorArray = new double[array.GetLength(0)][];

            for (int k = 0; k < array.GetLength(0); k++) //Array set up
            {
                VectorArray[k] = new double[28 * 28];
            }

            for (int i = 0; i < array.GetLength(0); ++i)
            {
                var temp = new int[28 * 28];
                temp = Utility.Array3DToVector(array, i);
                for (int j = 0; j < 28 * 28; j++)
                {
                    VectorArray[i][j] = temp[j];
                }
            }

            return VectorArray;
        }

        /// <summary>
        /// Get labelsArray for DeepLearning module.
        /// </summary>
        /// <param name="labels"></param>
        /// <returns></returns>
        public double[][] GetLebelsArray(int[] labels)
        {
            var labelsArray = new double[labels.Length][];

            for(int i = 0; i < labels.Length; ++i)
            {
                labelsArray[i] = new double[1]; //Array set up
                labelsArray[i][0] = labels[i]; //Add value
            }

            return labelsArray;
        }

    }
}