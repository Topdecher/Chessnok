using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.EventSystem
{
    public class EventBuffer
    {
        public enum Order
        {
            BeforeCurrentEvent,
            AfterCurrentEvent
        }

        private List<GameEvent> gameEvents;

        public GameEvent GetLastEvent()
        {
            if (gameEvents.Count > 0)
            {
                return gameEvents[0];
            }
            return null;
        }

        public void ClearLastEvent() 
        {
            if (gameEvents.Count > 0)
            {
                gameEvents.RemoveAt(0);
            }
        }

        public void ClearEvent(GameEvent gameEvent)
        {
            if (gameEvents.Contains(gameEvent))
            {
                gameEvents.Remove(gameEvent);
            }
        } 

        public void AddEvent(GameEvent gameEvent, Order order)
        {
            if (order == Order.BeforeCurrentEvent)
            {
                gameEvents.Insert(0, gameEvent);
            }
            else
            {
                gameEvents.Insert(1, gameEvent);
            }
        }

        //Adds line of events with same order of elements
        public void AddEventLine(GameEvent[] gameEvents, Order order)
        {
            if (order == Order.BeforeCurrentEvent)
            {
                for (int i = gameEvents.Length - 1; i > -1; i--)
                {
                    AddEvent(gameEvents[i], Order.BeforeCurrentEvent);
                }
            }
            else
            {
                for (int i = 0; i < gameEvents.Length; i++)
                {
                    AddEvent(gameEvents[i], Order.AfterCurrentEvent);
                }
            }
        }

        public int GetLength() => gameEvents.Count;

        public bool IsEmpty() => gameEvents.Count == 0;

        public void Activate() => gameEvents = new List<GameEvent>();

    }
}