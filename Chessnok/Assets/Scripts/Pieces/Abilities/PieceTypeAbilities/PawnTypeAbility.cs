using Assets.Scripts.EventSystem;
using Assets.Scripts.EventSystem.GameEvents;
using Assets.Scripts.Pieces.Abilities.OnPlayAbilities;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Pieces.Abilities.PieceTypeAbilities
{
    public class PawnTypeAbility : PieceTypeAbility
    {
        public override void ThrowEvent()
        {
            AbilityEvent abilityEvent = new AbilityEvent();
            abilityEvent.SetAbility(this);
            abilityEvent.SetExecutions(executionTimes);

            eventControl.AddEvent(abilityEvent, EventBuffer.Order.BeforeCurrentEvent);
            //Debug.Log("wtf PawnTypeAbility is throwing");
        }

        public override void Execute()
        {
            //Debug.Log("wtf PawnTypeAbilty executing");
            DrawCardEvent drawCardEvent = new DrawCardEvent();
            drawCardEvent.SetEventCaster(card);
            drawCardEvent.SetExecutions(executionTimes);
            eventControl.AddEvent(drawCardEvent, EventBuffer.Order.BeforeCurrentEvent);
            CurrentExecutions += 1;
        }

        public override void Activate()
        {
            executionTimes = 1;
            realType = typeof(OnPlayAbility);
        }
    }
}