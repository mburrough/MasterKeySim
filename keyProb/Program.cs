using System;
using System.Diagnostics;

namespace keyProb
{
    class Program
    {
        static void Main(string[] args)
        {
            int tests = 10000;  //How many tests to run
            int pinPositions = 7; //How many key stacks in this lock
            int depths = 6; //How many possible cut depths per pin.
            int buffer = 0; //How many cuts on either side of master are illegal. (E.g. disallow pin 'wafers' of this size).

            //Create a specific master key set to all 0's.
            int[] m = new int[pinPositions];
            for (int i = 0; i < m.Length; i++)
                m[i] = 0;


            Random r = new Random();    //Pass in random so we don't keep regenerate it each test run, which can lead to issues with the random values.
            double min = Double.MaxValue;
            double max = 0;
            double total=0;
            double result = 0;
            Stopwatch stopwatch = Stopwatch.StartNew();
            for (int tries = 0; tries < tests; tries++)
            {
                KeyTest k = new KeyTest(pinPositions, depths, buffer, r);
                //KeyTest k = new KeyTest(pinPositions, depths, buffer, m, r); // Optionally, provide a fixed master to test
                result = k.Simulation();
                total += result;
                if(min > result)
                {
                    min = result;
                }
                if(max < result)
                {
                    max = result;
                }
            }
            stopwatch.Stop();
            Console.WriteLine();
            double avg = total / tests;
            Console.WriteLine("Average: " + avg);
            Console.WriteLine("Minimum: " + min);
            Console.WriteLine("Maximum: " + max);
            Console.WriteLine("Test time: {0}", stopwatch.Elapsed);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
