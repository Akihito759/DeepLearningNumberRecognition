using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;


namespace DeepLearningNumberRecognition
{
    class Program
    {
        static void Main(string[] args)
        {
            

            try
            {
                var test = new DigitImage();
                test.ShowTestImages(true);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }



    } 
}