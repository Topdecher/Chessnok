using Assets.Scripts.States.CardStates;
using Assets.Scripts.EventSystem;
using Assets.Scripts.MenuEnvironment;
using Assets.Scripts.Pieces;
using Assets.Scripts.Util.FileStuff;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.PlayerEnvironment
{
    public class Deck : MonoBehaviour
    {
        private FileReader reader;
        private CardContainer allCards;

        private Vector3 deckCoords;
        
        private List<Card> cards;
        public GameObject cardPrefab;
        // this is deck face
        private GameObject blackDimakCard;

        public Hand hand;

        private EventControl eventControl;

        public void Start()
        {
            deckCoords = new Vector3(2, 0, -6);
            cards = new List<Card>();
            reader = new FileReader();
            allCards = new CardContainer();
            CreateDeck(18);
            //hand.DrawCard(3);
        }

        public void CreateDeck(int deckIndex)
        {
            List<string> cardNames = new List<string>();

            reader.Activate("C:/KovalenkoDB/Chessnok/Assets/Scripts/DataFiles/PlayerDecks.txt");
            int deckSize = int.Parse(reader.GetLine(deckIndex - 1)[1]);
            for (int i = 0; i < deckSize; i++)
            {
                string cardName = reader.GetLine(deckIndex + i)[1];
                cardNames.Add(cardName);
            }
            reader.SafeClose();

            List<System.Type> cardPrefabs = allCards.GetCards(cardNames);
            //List<System.Type> cardPrefabs = new List<System.Type>();
            InstantiateCards(cardPrefabs, cardNames);
            InstantiateDeckUp();
        }

        public void InstantiateCards(List<System.Type> cardPrefabs, List<String> cardNames)
        {
            for (int i = 0; i < cardPrefabs.Count; i++)
            {
                Card deckCard;
                Type cardType = Type.GetType(GetFullName(cardNames[i], "E2"));
                if (cardType == null)
                {
                    cardType = Type.GetType(GetFullName(cardNames[i], "Base"));
                }
                //Assets.Scripts.Pieces.SetE2_Remastered.Queens.
                if (cardType == null)
                {
                    Debug.Log("wtf");
                    deckCard = Instantiate(cardPrefab).AddComponent(typeof(Piece)) as Card;
                }
                else
                {
                    deckCard = Instantiate(cardPrefab).AddComponent(cardType) as Card;
                }
                //DELETE
                deckCard.SetName(cardNames[i]);

                deckCard.SetEventControl(eventControl);
                deckCard.OnAttachment();
                deckCard.ChangeState(typeof(DeckState));
                deckCard.GetComponent<MeshRenderer>().sortingLayerName = "Hand";
                deckCard.InstantMove(deckCoords);
                deckCard.FaceDown();
                deckCard.Disable();
                cards.Add(deckCard);
            }
            Reshuffle();
        }

        public void InstantiateDeckUp()
        {
            Texture2D BlackDimakTexture = Resources.Load<Texture2D>("Textures/BlackDimakCard_texture");
            blackDimakCard = Instantiate(cardPrefab, deckCoords, Quaternion.Euler(0f, 180f, 0f));
            blackDimakCard.GetComponent<Renderer>().material.mainTexture = BlackDimakTexture;
            GameObject cardAnchor = new GameObject();
            cardAnchor.transform.position = deckCoords;
            blackDimakCard.transform.parent = cardAnchor.transform;
            blackDimakCard.transform.localPosition = new Vector3(0.25f, 0f, 0f);
        }

        public void CheckLastCard()
        {
            if (cards.Count <= 0 & blackDimakCard.activeSelf == true)
            {
                blackDimakCard.SetActive(false);
            }
            else
            {
                if (blackDimakCard.activeSelf == false)
                {
                    blackDimakCard.SetActive(true);
                }
            }
        }

        public void Reshuffle()
        {
            List<Card> temporalList = new List<Card>();
            for (int i = 0; i < cards.Count; i++)
            {
                int randInt = UnityEngine.Random.Range(0, i + 1);
                temporalList.Insert(randInt, cards[i]);
            }
            cards = temporalList;
        }

        public void RandInsert(Card card)
        {
            int randInt = UnityEngine.Random.Range(0, cards.Count + 1);
            cards.Insert(randInt, card);
        }

        public void RandShuffle(Card card)
        {
            cards.Add(card);
            Reshuffle();
        }

        public Card GetUpCard()
        {
            if (IsEmpty())
            {
                return null;
            }
            else
            {
                return cards[0];
            }
        }

        public Card GetCard(int index)
        {
            if (index < cards.Count)
            {
                return cards[index];
            }
            else
            {
                return null;
            }
        }

        public bool IsEmpty()
        {
            return cards.Count == 0;
        }

        public void RemoveCard(Card card)
        {
            if (cards.Contains(card))
            {
                cards.Remove(card);
                CheckLastCard();
            }
        }

        public String GetFullName(string cardName, string setName)
        {
            string fullSetName = "";
            switch (setName)
            {
                case "Base":
                    fullSetName = "BaseSet.";
                    break;
                case "E2":
                    fullSetName = "SetE2_Remastered.";
                    break;
                case "E4":
                    fullSetName = "SetE4.";
                    break;
            }
            string returnName = "Assets.Scripts.Pieces." + fullSetName + GetCardType(cardName) + cardName;
            return returnName;
        }

        private string GetCardType(string cardName)
        {
            string cardType = "";
            for (int i = cardName.Length - 1; i > -1; i--)
            {
                if (cardName[i].ToString().ToUpper() == cardName[i].ToString())
                {
                    cardType = cardName.Substring(i) + "s.";
                    break;
                }
            }
            return cardType;
        }


        public void SetEventControl(EventControl eventControl) => this.eventControl = eventControl;
        public EventControl GetEventControl() => eventControl;
    }
}