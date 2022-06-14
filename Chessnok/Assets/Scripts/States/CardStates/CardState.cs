using System.Collections;
using UnityEngine;

namespace Assets.Scripts.States.CardStates
{
    public class CardState : MonoBehaviour
    {
        protected Card card;

        public virtual void UpdateState()
        {
        }

        public void SetCard(Card card) => this.card = card;
    }
}