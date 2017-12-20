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


        /// <summary>
        /// Transform int[][][] imageArray and int[] labels to dobule[][]. length = 0 => take all array.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="label"></param>
        /// <param name="length"></param>
        public MistToNeutralNetworkAdapter(int[][][] array,int[] label,int length=0)
        {
            if (array.GetLength(0) == label.Length)
            {
                ImagesVectorArray = GetImagesVectorsArray(array,length);
                LabelsArray = GetLebelsArray(label, length);
            }
        }

        /// <summary>
        /// Get ImageVectorsArray for DeepLearning module.
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public double[][] GetImagesVectorsArray(int[][][] array,int length=0)
        {
            if (length == 0)
            {
                length = array.GetLength(0);
            }
                

            var VectorArray = new double[length][];

            for (int k = 0; k < length; k++) //Array set up
            {
                VectorArray[k] = new double[28 * 28];
            }

            for (int i = 0; i < length; ++i)
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
        public double[][] GetLebelsArray(int[] labels,int length=0)
        {
            if (length == 0)
            {
                length = labels.Length;
            }
            var labelsArray = new double[length][];

            for(int i = 0; i < length; ++i)
            {
                labelsArray[i] = new double[10]; //Array set up
                labelsArray[i][labels[i]] = 1; //Add value
            }

            return labelsArray;
        }

    }
}