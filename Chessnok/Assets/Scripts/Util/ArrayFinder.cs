using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Util
{
    public class ArrayFinder
    {
        private string[] strArray;
        private List<string> strList;
        private int[] intArray;
        private List<int> intList;

        private const string letters = "abcdefghijklmnopqrstuvwxyz";


        //TODO DELETE FIRST CONDITION IN WHILE
        public int Find(string word, string[] words)
        {
            int startInd = -1;
            int lastInd = words.Length;
            int middle = (startInd + lastInd) / 2;
            while (words[middle] != word & startInd < lastInd - 1)
            {
                if (AlphabeticLesser(word, words[middle]))
                {
                    lastInd = middle;
                    middle = (startInd + lastInd) / 2;
                }
                else
                {
                    startInd = middle;
                    middle = (startInd + lastInd) / 2;
                }
            }
            if (word != words[middle])
            {
                return -1;
            }
            else
            {
                return middle;
            }
        }

        public int Find(string word, List<string> words)
        {
            int startInd = -1;
            int lastInd = words.Count;
            int middle = (startInd + lastInd) / 2;
            while (words[middle] != word & startInd < lastInd - 1)
            {
                if (AlphabeticLesser(word, words[middle]))
                {
                    lastInd = middle;
                    middle = (startInd + lastInd) / 2;
                }
                else
                {
                    startInd = middle;
                    middle = (startInd + lastInd) / 2;
                }
            }
            if (word != words[middle])
            {
                return -1;
            }
            else
            {
                return middle;
            }
        }

        public int Find(int number, int[] numbers)
        {
            int startInd = -1;
            int lastInd = numbers.Length;
            int middle = (startInd + lastInd) / 2;
            while (numbers[middle] != number & startInd < lastInd - 1)
            {
                if (number < numbers[middle])
                {
                    lastInd = middle;
                    middle = (startInd + lastInd) / 2;
                }
                else
                {
                    startInd = middle;
                    middle = (startInd + lastInd) / 2;
                }
            }
            if (number != numbers[middle])
            {
                return -1;
            }
            else
            {
                return middle;
            }
        }

        public int Find(int number, List<int> numbers)
        {
            int startInd = -1;
            int lastInd = numbers.Count;
            int middle = (startInd + lastInd) / 2;
            while (numbers[middle] != number & startInd < lastInd - 1)
            {
                if (number < numbers[middle])
                {
                    lastInd = middle;
                    middle = (startInd + lastInd) / 2;
                }
                else
                {
                    startInd = middle;
                    middle = (startInd + lastInd) / 2;
                }
            }
            if (number != numbers[middle])
            {
                return -1;
            }
            else
            {
                return middle;
            }
        }

        public bool AlphabeticLesser(string word1, string word2)
        {
            int lastIter = Mathf.Min(word1.Length, word2.Length);
            word1 = word1.ToLower();
            word2 = word2.ToLower();
            for (int i = 0; i < lastIter; i++)
            {
                int index1 = letters.IndexOf(word1[i]);
                int index2 = letters.IndexOf(word2[i]);
                if(index1 != index2)
                {
                    return index1 < index2 ? true : false;
                }
            }
            if (word1 != word2)
            {
                return word1.Length < word2.Length ? true : false;
            }
            return true;
        }
    }
}