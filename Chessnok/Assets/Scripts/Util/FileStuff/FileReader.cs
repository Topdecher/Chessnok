using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Util.FileStuff
{
    public class FileReader
    {
        private StreamReader reader;
        private ArrayFinder finder;
        private List<string[]> lineBuffer;
        private List<string> allWords;
        private string path;

        public void SetPath(string path) => this.path = path;

        public void ReadAllLines()
        {
            try
            {
                reader = new StreamReader(path);
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lineBuffer.Add(line.Split(' '));
                    foreach (string word in line.Split(' '))
                    {
                        allWords.Add(word);
                    }
                }
            }
            catch (IOException)
            {
                // smth went wrong
            }
            SafeClose();
        }

        public void SafeClose()
        {
            try
            {
                reader.Close();
            }
            catch (Exception)
            {
                // file wasn't opened
            }
        }

        public void Activate(string path)
        {
            lineBuffer = new List<string[]>();
            allWords = new List<string>();
            SetPath(path);
            ReadAllLines();
        }

        public string[] GetLine(int index) => lineBuffer[index];

        public string GetWord(int lineInd, int wordInd) => lineBuffer[lineInd][wordInd];

        public List<string> GetAllWords() => allWords;
    }
}