using Assets.Scripts.States.CardStates;
using Assets.Scripts.DragHandlers;
using Assets.Scripts.EventSystem;
using Assets.Scripts.EventSystem.GameEvents;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.PlayerEnvironment
{
    public class Hand : MonoBehaviour
    {
        public List<Card> cards;
        public HandDragHandler dragHandler;
        
        private EventControl eventControl;
        private HandEventHandler eventHandler;

        private int handSize;
        private int tooFckingMuch;
        private float maxLength;
        private Vector3 projectiveCenter;
        
        public float cardWidth;
        public float maxCardGap;
        public Vector3 cardAnchor;
        public Card cardPrefab;
        public Deck deck;

        //DELETE
        private bool spaceWasPressed = false;

        private void Start()
        {
            SetBaseParams();
            eventHandler = new HandEventHandler();
            eventHandler.SetEventControl(eventControl);
            eventHandler.Activate();
            eventControl.AddEventListenerHandler(eventHandler);
        }

        private void Update()
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame & !spaceWasPressed)
            {
                DrawCard(7);
                spaceWasPressed = true;
            }
        }

        public void Rearrange()
        {
            if (handSize == 1)
            {
                cards[0].NextPosition = cardAnchor;
                return;
            }
            if (handSize >= tooFckingMuch)
            {
                return;
            }

            float startCoord;
            float step;
            if (maxLength/(handSize-1) > cardWidth)
            {
                float extraSpace = maxLength + cardWidth - cardWidth * handSize;
                float currentGap = extraSpace / (handSize - 1);
                float cardGap = Math.Min(currentGap, maxCardGap);
                startCoord = -(handSize - 1) * (cardGap + cardWidth)/2 + cardAnchor.x;
                step = cardWidth + cardGap;
            }
            else
            {
                step = maxLength / (handSize - 1);
                startCoord = -maxLength / 2 + cardAnchor.x;
            }
            for (int i = 0; i < handSize; i++)
            {
                Vector3 newPos = new Vector3(startCoord + step * i, cardAnchor.y, cardAnchor.z);
                cards[i].NextPosition = newPos;
                cards[i].GetComponent<MeshRenderer>().sortingOrder = handSize - i - 1;
                cards[i].SetBaseSortingOrder(handSize - i - 1);
                cards[i].SetHandPosition(newPos);
            }
        }

        public void SetBaseParams()
        {
            handSize = 0;
            tooFckingMuch = 20;
            maxLength = 5.5f;
            maxCardGap = 0.1f;
            cardWidth = 0.5f;
            cards = new List<Card>();
            projectiveCenter = new Vector3(0f, 0.8f, -10f) * 7f/10f;
        }

        public void DrawCard()
        {
            Card newCard = deck.GetUpCard();
            if (newCard != null)
            {
                newCard.Enable();
                newCard.ChangeState(typeof(HandState));
                newCard.SetHand(this);
                newCard.SetDragHandler(dragHandler);
                cards.Add(newCard);
                deck.RemoveCard(newCard);
                UpdateSize();

                newCard.FaceUp();
                eventHandler.AddListener(newCard);
                Rearrange();

                //DrawCardEvent drawCardEvent = new DrawCardEvent();
                //drawCardEvent.SetEventCaster(this);
                //eventControl.AddEvent(drawCardEvent, EventBuffer.Order.BeforeCurrentEvent);
            }
        }

        public void DrawCard(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                DrawCard();
            }
        }

        public void RemoveCard(Card card)
        {
            cards.Remove(card);
            UpdateSize();
            Rearrange();
        }

        public void UpdateSize()
        {
            handSize = cards.Count;
        }

        public Vector3 EnlargePoint() => projectiveCenter;

        public void SetEventControl(EventControl eventControl) => this.eventControl = eventControl;
        public EventControl GetEventControl() => eventControl;
    }
}