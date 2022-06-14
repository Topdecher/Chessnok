using Assets.Scripts.EventSystem;
using Assets.Scripts.Interfaces;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Pieces
{
    public class Ability : IEventListener
    {
        protected Card card;
        protected BoardPiece boardPiece;
        protected EventControl eventControl;
        
        protected int executionTimes = 1;

        public int CurrentExecutions { get; set; } = 0;

        public virtual bool HasCondition { get; set; } = false;

        public virtual bool IsDeaf { get; set; } = true;

        public virtual void Activate(int executionTimes)
        {
            this.executionTimes = executionTimes;
        }

        public virtual void Activate()
        {
            executionTimes = 1;
        }

        public void SetCard(Card card) => this.card = card;

        public EventControl GetEventControl() => card.GetEventControl();
        public void SetEventControl(EventControl eventControl) => this.eventControl = eventControl;

        public virtual void Listen(GameEvent gameEvent)
        {
            if (IsDeaf)
            {
                return;
            }
        }

        public virtual void Execute() { }

        public virtual void ThrowEvent() { }

        public virtual void CheckCondition() { }

        public bool IsWasted() => executionTimes <= CurrentExecutions;

        public virtual int GetExecutionTimes() => executionTimes;

        //public virtual void Play() { }
    }
}