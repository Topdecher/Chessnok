using Assets.Scripts.Pieces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.EventSystem
{
    public class GameEvent
    {
        public enum EventType
        {
            CardPlay,
            CardDraw,
            CardShuffle,
            PieceAttack,
            PieceMove,
            PieceRotate,
            PieceEats,
            PieceEaten,
            PieceReceiveToken,
            Ability,
            Gambit,
            Check,
            Checkmate,
            TurnStart,
            TurnEnd
        }

        protected EventType eventType;
        protected GameEvent chainedEvent;
        protected Card eventCaster;
        protected BoardPiece eventPieceCaster;
        protected int executionTimes;

        public bool IsPerformed { get; set; }

        public int CurrentExecutions { get; set; } = 0;

        public Card GetEventCaster() => eventCaster;

        public void SetEventCaster(Card eventCaster) => this.eventCaster = eventCaster;

        public virtual void Execute()
        {

        }

        public virtual bool IsWasted() => executionTimes <= CurrentExecutions;

        public virtual void SetExecutions(int executionTimes) => this.executionTimes = executionTimes;
    }
}