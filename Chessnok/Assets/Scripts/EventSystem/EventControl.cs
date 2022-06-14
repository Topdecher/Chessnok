using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.EventSystem
{
    public class EventControl : MonoBehaviour
    {
        public enum GameState
        {
            MulligunState,
            StartGameState,
            TurnGameState,
            StartTurnState,
            PlayerTurnState,
            EndTurnState,
            EndGameState
        }

        private EventBuffer eventBuffer;

        private List<EventListenerHandler> eventListenerHandlers;
        private int executedEventLoopEvents;
        private int executedEventLoopEventsMax;
        
        private int whileCount;
        private int whileMaxCount;
        
        private bool isInEventLoop;
        private GameState gameState;

        private void Awake()
        {
            eventListenerHandlers = new List<EventListenerHandler>();
            eventBuffer = new EventBuffer();
            eventBuffer.Activate();
            executedEventLoopEvents = 0;
            executedEventLoopEventsMax = 100;
            isInEventLoop = false;

            whileCount = 0;
            whileMaxCount = 200;
        }

        private void Update()
        {
            UpdateEvents();
        }

        public void UpdateEvents()
        {
            //Debug.Log(eventBuffer.GetLength());
            executedEventLoopEvents = 0;
            whileCount = 0;
            while (IsUpdatable() & whileCount <= whileMaxCount)
            {
                //Debug.Log("wtf eventControlUpdating");
                //logic
                GameEvent currentEvent = eventBuffer.GetLastEvent();
                if (!currentEvent.IsWasted())
                { 
                    CheckEventListeners(currentEvent);
                    //Debug.Log("Checked");
                }
                else
                {
                    eventBuffer.ClearLastEvent();
                    //Debug.Log("Cleared");
                }

                if (currentEvent == eventBuffer.GetLastEvent())
                {
                    //Debug.Log(currentEvent.GetType());
                    currentEvent.Execute();
                    currentEvent.CurrentExecutions += 1;
                    executedEventLoopEvents += 1;
                    if (currentEvent.IsWasted())
                    {
                        eventBuffer.ClearEvent(currentEvent);
                    }
                }
                whileCount += 1;
            }

            if (eventBuffer.IsEmpty())
            {
                isInEventLoop = false;
            }
            else
            {
                isInEventLoop = true;
            }
        }

        private bool IsUpdatable()
        {
            return (executedEventLoopEvents < executedEventLoopEventsMax) & !eventBuffer.IsEmpty();
        }

        public void CheckEventListeners(GameEvent gameEvent)
        {
            foreach (EventListenerHandler eventListenerHandler in eventListenerHandlers)
            {
                if (eventListenerHandler != null)
                {
                    eventListenerHandler.ListenTo(gameEvent);
                }
            }
        }

        public bool IsInEventLoop() => isInEventLoop;

        public EventBuffer GetEventBuffer() => eventBuffer;

        public void AddEvent(GameEvent gameEvent, EventBuffer.Order order)
        {
            eventBuffer.AddEvent(gameEvent, order);
        }
        
        public void AddEventLine(GameEvent[] gameEvents, EventBuffer.Order order)
        {
            eventBuffer.AddEventLine(gameEvents, order);
        }

        public void AddEventListenerHandler(EventListenerHandler eventListenerHandler)
        {
            eventListenerHandlers.Add(eventListenerHandler);
        }
    }
}