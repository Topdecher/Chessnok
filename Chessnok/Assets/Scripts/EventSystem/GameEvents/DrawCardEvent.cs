using Assets.Scripts.PlayerEnvironment;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.EventSystem.GameEvents
{
    public class DrawCardEvent : GameEvent
    {
        protected Card drawnСard;

        public override void Execute() 
        {
            eventCaster.GetHand().DrawCard();
        }
    }
}