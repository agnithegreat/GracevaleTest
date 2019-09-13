using System;
using System.Collections.Generic;

namespace Tools
{
    public static class RandomizeHelper
    {
        public static List<int> Randomize(int inputLength, int count, bool allowDuplicates)
        {
            var inputList = new List<int>();
            for (var i = 0; i < inputLength; i++)
            {
                inputList.Add(i);
            }
            
            var output = new List<int>();
            var random = new Random();
            for (var i = 0; i < count; i++)
            {
                if (inputList.Count == 0) return output;
                
                var rand = random.Next(inputList.Count);
                output.Add(inputList[rand]);
                
                if (!allowDuplicates)
                {
                    inputList.RemoveAt(rand);
                }
            }
            return output;
        }
    }
}