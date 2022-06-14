using Assets.Scripts.Pieces;
using Assets.Scripts.Util;
using Assets.Scripts.Util.FileStuff;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.MenuEnvironment
{
    public class CardContainer
    {
        private FileReader reader;
        private ArrayFinder finder;
        private string allCardsPath;

        List<System.Type> allCards = new List<System.Type>()
        {
            typeof(Piece)
        };

        public List<System.Type> GetCards(List<string> cardNames)
        {
            // DELETE
            reader = new FileReader();
            finder = new ArrayFinder();

            reader.Activate("C:/KovalenkoDB/Chessnok/Assets/Scripts/DataFiles/AllCards.txt");
            List<string> allNames = reader.GetAllWords();
            reader.SafeClose();

            List<System.Type> outCards = new List<System.Type>();
            foreach (string cardName in cardNames)
            {
                int cardInd = finder.Find(cardName, allNames);
                if (cardInd == -1)
                {
                    outCards.Add(typeof(Piece));
                }
                else
                {
                    outCards.Add(allCards[cardInd]);
                }
            }
            return outCards;
        }
    }
}