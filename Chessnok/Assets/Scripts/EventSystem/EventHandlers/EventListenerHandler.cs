using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.EventSystem
{
    public class EventListenerHandler
    {
        protected EventControl eventControl;
        protected List<Card> activeListeners;

        public void ListenTo(GameEvent gameEvent)
        {
            foreach (Card listener in activeListeners)
            {
                if (listener != null)
                {
                    listener.Listen(gameEvent);
                }
            }
        }

        public void SetEventControl(EventControl eventControl) => this.eventControl = eventControl;

        public void AddListener(Card card) => activeListeners.Add(card);

        public void RemoveListener(Card card) => activeListeners.Remove(card);

        public void Activate()
        {
            activeListeners = new List<Card>();
        }
    }
}