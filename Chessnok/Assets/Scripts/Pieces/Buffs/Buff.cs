using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Pieces.Buffs
{
    public class Buff
    {
        protected int powerBuff;
        
        public bool IsPassive { get; set; }

        public virtual int GetPowerBuff()
        {
            return 0;
        }


    }
}