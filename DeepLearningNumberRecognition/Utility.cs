using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepLearningNumberRecognition
{
    public static class Utility
    {
        /// <summary>
        /// Get single vector of 2D image based on [image index][28][28].
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static int[] Array3DToVector(int[][][] array, int index)
        {
            var vector = new int[28 * 28];
            int position = 0;

            for (int i = 0; i < 28; i++)
            {
                for (int j = 0; j < 28; j++)
                {
                    vector[position] = array[index][i][j];
                    position++;
                }
                
            }
            return vector;
        }
       
    }
}
