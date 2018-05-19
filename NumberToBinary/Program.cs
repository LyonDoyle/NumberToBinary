using System;

namespace BinaryCalculatorApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //
            // This example shows the power of loops. Instead of hard coding values and if/else control statements for each value
            // a loop is used instead. More work can be done, and more efficiently, using DRY (Don't Repeat Yourself). 
            //
            // The primary reason to not repeat code is:
            //
            //  1) It is more maintainable and less error prone if you remove duplicate code.
            //     If you have to change the mechanics of your code, you do so in fewer places.
            //
            //  2) It is faster to write and easier to understand less code. :)
            //
            bool continueUsingApp = true;
            uint[] bitArray = GenerateBitArray(32);
            string errorMessage = "Invalid number entered. (positive number from 0 to 2,147,483,647)";

            //
            // a loop is used here so the user can enter as many numbers as they would like, 
            // or if user enters an invalid number, they do not have to restart the app to try again.
            //
            while (continueUsingApp)
            {
                Console.Write("Enter any positive number from 0 to 2,147,483,647: ");

                //
                // TryParse will attempt to parse the user input string, 
                // returning a boolean value indicating whether a positive number from 0 to Int32.MaxValue (2,147,483,647) was entered.
                // "out uint userInput" will assign the numeric value of a successful parse to a unsigned integer variable (userInput).
                //
                if (UInt32.TryParse(Console.ReadLine(), out uint userInput))
                {
                    if (userInput > Int32.MaxValue)
                    {
                        Console.WriteLine(errorMessage);
                    }
                    else
                    {
                        string int32AsBinary = BinaryCalculator(userInput, bitArray);
                        Console.WriteLine(userInput.ToString() + " as binary is: " + int32AsBinary);
                    }
                }
                else
                {
                    Console.WriteLine(errorMessage);
                }

                Console.Write("Press 'Y' to continue or 'N' to exit.");
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                continueUsingApp = keyInfo.Key == ConsoleKey.Y;
                Console.Clear();
            }
        }

        /// <summary>
        /// Write any number from 0 to 2,147,483,647 as binary.
        /// </summary>
        /// <param name="number">An unsigned integer in which to display as a binary number.</param>
        /// <param name="bitArray"></param>
        /// <returns></returns>
        private static string BinaryCalculator(uint number, uint[] bitArray)
        {
            string output = "";
            uint remainder = number;
            int spaceNeeded = 0;

            // 
            // this for loop begins with last position of the unsigned integer array (a value of 2,147,483,648) and counts backward.
            //
            for (int i = bitArray.Length - 1; i >= 0; i--)
            {
                uint n = bitArray[i];

                //
                // used to space out binary string every 4 bits for easier readability.
                //
                if (spaceNeeded == 4)
                {
                    spaceNeeded = 0;
                    output += " ";
                }
                spaceNeeded++; // end spacing

                if (n == 1)
                {
                    if (remainder == 1)
                    {
                        output += "1";
                    }
                    else
                    {
                        output += "0";
                    }
                }
                else if (remainder < n)
                {
                    output += "0";
                }
                else
                {
                    output += "1";
                    remainder = remainder - n;
                }
            }
            return output;
        }

        /// <summary>
        /// Creates an array of unsigned integers starting at 1 and doubling every value until 2,147,483,648 (large enough to process the maximum Int32 value).
        /// </summary>
        /// <param name="size">32 is the maximum size possible using unsigned integer values.</param>
        /// <returns></returns>
        private static uint[] GenerateBitArray(int size)
        {
            uint[] bitArray = new uint[size];
            uint bit = 1;
            for (int i = 0; i < size; i++)
            {
                bitArray[i] = bit;
                bit = bit * 2;
            }
            return bitArray;
        }
    }
}
