using System.Collections;
using UnityEngine;

namespace Assets.Scripts.States.CardStates
{
    public class HandState : CardState
    {
        public override void UpdateState()
        {
            if (card.IsDragged)
            {
                card.OnDrag();
                return;
            }

            if (card.IsMouseOver)
            {
                card.MouseOver();
            }
        }
    }
}