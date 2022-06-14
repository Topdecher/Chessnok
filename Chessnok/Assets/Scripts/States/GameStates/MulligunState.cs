using Assets.Scripts.Pieces;
using Assets.Scripts.PlayerEnvironment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.States.GameStates
{
    public class MulligunState : GameState
    {
        public enum RearrangeMode 
        {
            Drawn,
            Taken
        }

        private const float GAP = 0.2f;
        private const float CARD_WIDTH = 1f;
        private const float MAX_RANGE = 6f * (CARD_WIDTH + GAP);

        private List<Card> drawnCards;
        private List<Card> takenCards;
        private Deck deck;
        
        private Vector3 rearrangeTakenAnchor;
        private Vector3 rearrangeDrawnAnchor;


        public override void StartState()
        {
            drawnCards = new List<Card>();
            takenCards = new List<Card>();
            DrawCards(7);
            if (!DrawnHasPawn())
            {
                DrawCards(2);
            }
        }

        public void RearrangeCards(RearrangeMode rearrangeMode)
        {
            Vector3 anchor;
            List<Card> cards;
            if (rearrangeMode == RearrangeMode.Drawn)
            {
                anchor = rearrangeDrawnAnchor;
                cards = drawnCards;
            }
            else
            {
                anchor = rearrangeTakenAnchor;
                cards = takenCards;
            }

            Vector3 shiftVector;
            if (cards.Count <= 7)
            {
                shiftVector = new Vector3(CARD_WIDTH + GAP, 0f, 0f);
            }
            else
            {
                shiftVector = new Vector3(MAX_RANGE / (cards.Count - 1), 0f, 0f);    
            }

            for (int i = 0; i < cards.Count; i++)
            {
                int shiftsAmount = i - (cards.Count - 1) / 2;
                cards[i].NextPosition = anchor + shiftVector * shiftsAmount;
                cards[i].SetSortingOrder(cards.Count - i - 1);
            }
        }

        public void DrawCards(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Card newCard = deck.GetCard(i);
                newCard.Enable();
                drawnCards.Add(newCard);
            }
            RearrangeCards(RearrangeMode.Drawn);
        }

        public bool DrawnHasPawn()
        {
            foreach (Card drawnCard in drawnCards)
            {
                //typeof(Pawn).IsSubclassOf(drawnCard.GetType())
                if (drawnCard.GetType() == typeof(Pawn))
                {
                    return true;
                }
            }
            return false;
        }


    }
}