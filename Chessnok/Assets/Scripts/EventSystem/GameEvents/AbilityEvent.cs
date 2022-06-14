using Assets.Scripts.Pieces;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.EventSystem.GameEvents
{
    public class AbilityEvent : CardEvent
    {
        protected Ability ability;

        public void SetAbility(Ability ability) => this.ability = ability;

        public override void Execute() 
        {
            //Debug.Log(ability.GetType());
            ability.Execute(); 
        }
    }
}