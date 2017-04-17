﻿using System;

namespace keyProb
{
    class KeyTest
    {
        private Random rnd;
        private int[] master;
        int TestedKeys;
        private int Buffer;
        private int PinCount;
        private int Depths;
        bool[,] testMap;

        /**
         * KeyTest
         * Sets up a new test.
         * Params:
         *   pinCount - total number of pin stacks in the lock
         *   depths - number of possible cut depths per pin
         *   buffer - number of pins disallowed around master. 0 means a pin wafer of size 1 is allowed. Must be >= 0.
         **/
        public KeyTest(int pinCount, int depths, int buffer)
        {
            rnd = new Random();
            PinCount = pinCount;
            Depths = depths;
            Buffer = buffer;
            TestedKeys = 0;
            GenerateMaster();
            BuildTestMap();
        }

        /**
         * KeyTest
         * Sets up a new test.
         * Params:
         *   pinCount - total number of pin stacks in the lock
         *   depths - number of possible cut depths per pin
         *   buffer - number of pins disallowed around master. 0 means a pin wafer of size 1 is allowed. Must be >= 0.
         *   masterKey - if you want to test against a specific master key, instead of a randomly generated one, provide one here. 
         **/
        public KeyTest(int pinCount, int depths, int buffer, int[] masterKey)
        {
            rnd = new Random();
            PinCount = pinCount;
            Depths = depths;
            Buffer = buffer;
            TestedKeys = 0;
            master = masterKey;
            BuildTestMap();
        }

        // Creates a new random master key and prints it to the screen
        private void GenerateMaster()
        {
            Console.Write("Master: ");
            master = new int[PinCount];
            for (int i = 0; i < PinCount; i++)
            {
                master[i] = rnd.Next(Depths);
                Console.Write(master[i] + " ");
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        // Creates a boolean 2D array of size pin count x depths. 
        // Populates the array with all falses, except for the master pinning and 
        // any pins that would be disallowed by the buffer rule.
        private void BuildTestMap()
        {
            testMap = new bool[PinCount, Depths];
            for (int i = 0; i < PinCount; i++)
            {
                for (int j = 0; j < Depths; j++)
                {
                    testMap[i, j] = false;
                }
            }

            for (int i = 0; i < PinCount; i++)
            {
                //testMap[i, master[i]] = true; <-- Don't need to do this, since the first pass through the loop takes care of the master values.
                for (int k = 0; k <= Buffer; k++)
                {
                    if ((master[i] - k) >= 0)
                        testMap[i, master[i] - k] = true;
                    if ((master[i] + k) < Depths)
                        testMap[i, master[i] + k] = true;
                }
            }
        }

        //Display the current value of the test matrix
        private void printMap()
        {
            Console.Write("Dpth ");
            for (int z = 0; z < Depths; z++)
                Console.Write(z + " ");
            Console.WriteLine();
            for (int z = 0; z < Depths; z++)
                Console.Write("--");
            Console.WriteLine();
            for (int i = 0; i < PinCount; i++)
            {
                Console.Write("Pin" + i + " ");
                for (int j = 0; j < Depths; j++)
                {
                    Console.Write((testMap[i, j] ? 1 : 0) + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        //See if all possible options have been evaluated
        private bool checkFinished()
        {
            for (int i = 0; i < PinCount; i++)
            {
                for (int j = 0; j < Depths; j++)
                {
                    if (testMap[i,j] == false)
                        return false;
                }
            }
            return true;
        }

        //Update the test matrix with a new key
        private void AddKey(int[] key)
        {
            for(int i=0; i<key.Length; i++)
            {
                testMap[i, key[i]] = true;
            }
        }

        //Run the simulation
        public int Simulation()
        {
            do
            {
                AddKey(GenerateValidKey());
            } while (!checkFinished());
            Console.WriteLine();
            Console.WriteLine("Done in {0} keys.", TestedKeys);
            return TestedKeys;
        }

        //Create a new operator key. Key must not have any pins equal to master, nor any +/- buffer from master.
        private int[] GenerateValidKey()
        {
            int[] key = new int[PinCount];

            for (int i = 0; i < PinCount; i++)
            {
                do
                {
                    key[i] = rnd.Next(Depths);
                } while (!isValid(i, key[i]));
            }

            Console.Write("Key" + TestedKeys + ": \t");
            for(int i=0; i<PinCount; i++)
            {
                Console.Write(key[i] + " ");
            }
            Console.WriteLine();

            TestedKeys++;

            return key;
        }

        //Check if a given pin value is too close to the master value
        private bool isValid(int pos, int val)
        {
            if (master[pos] == val)
                return false;

            if (master[pos] == (val + Buffer))
                return false;

            if (master[pos] == (val - Buffer))
                return false;

            return true;
        }
    }
}