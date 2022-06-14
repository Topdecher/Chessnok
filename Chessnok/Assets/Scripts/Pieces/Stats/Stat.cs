using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Pieces.Stats
{
    public class Stat : MonoBehaviour
    {
        private const int STAT_CAP = 99;
        private int originalValue;
        private int currentValue;
        private int thisTurnBuff;

        public int GetOriginalValue() => originalValue;
        public int GetValue() => currentValue;
        public void SetValue(int value) 
        {
            if (value < 0)
            {
                currentValue = 0;
            }
            else if (value > STAT_CAP)
            {
                currentValue = STAT_CAP;
            }
            else
            {
                currentValue = value;
            }
        }
    }
}