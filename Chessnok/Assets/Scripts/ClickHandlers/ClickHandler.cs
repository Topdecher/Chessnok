using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.ClickHandlers
{
    public class ClickHandler : MonoBehaviour
    {
        private List<Card> cardList;

        private Card mouseOverCard;
        private Card potencialCard;

        private Camera playerCamera;
        private Vector2 mouseScreenPos;

        private void Update()
        {
            UpdateMousePos();
            CheckMouseOverCard();

            if (Mouse.current.leftButton.wasPressedThisFrame & mouseOverCard != null) 
            {
                potencialCard = mouseOverCard;
            }
            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                if (mouseOverCard != null & mouseOverCard == potencialCard)
                {
                    //smth happen with potencial card clicked
                }
            }
        }

        public void CheckMouseOverCard()
        {
            Vector3 point = GetMouseWorldPos(-6f);
            int sortingOrder = -1;
            foreach (Card card in cardList)
            {
                if (card.IsPointIn(point) & card.GetComponent<MeshRenderer>().sortingOrder > sortingOrder)
                {
                    mouseOverCard = card;
                    sortingOrder = card.GetComponent<MeshRenderer>().sortingOrder;
                }
            }
        }

        public void UpdateMousePos()
        {
            mouseScreenPos = Mouse.current.position.ReadValue();
        }

        public Vector3 GetMouseWorldPos(float depth)
        {
            Vector3 screenPos = new Vector3(mouseScreenPos.x, mouseScreenPos.y, depth + 10f);
            //Debug.Log(playerCamera.ScreenToWorldPoint(screenPos));
            return playerCamera.ScreenToWorldPoint(screenPos);
        }
    }
}