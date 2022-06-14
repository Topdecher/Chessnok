using Assets.Scripts.BoardEnvironment;
using Assets.Scripts.PlayerEnvironment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.DragHandlers
{
    public class HandDragHandler : DragHandler
    {
        private Card draggedCard;
        private Card mouseOverCard;
        public Hand hand;
        public Board board;
        public Cell potentialCell = null;

        //private PlayerInput playerInput;
        //private InputAction mouseRelease;

        private void Start()
        {
            draggedCard = null;
            mouseOverCard = null;
            dragManager = new DragManager();
            //orthographicCamera.enabled = false;
        }

        private void Update()
        {
            UpdateMousePos();

            if (!IsUpdatable())
            {
                return;
            }

            if (!dragManager.GetIsDragging())
            {
                MouseOverCheck();
            }
            DragCheck();

            if (isDragging)
            {
                CellCheck();
            }
        }

        //HOLY SHIT
        //BAD CODE
        public override void MouseOverCheck()
        {
            Card temporalCard = FindMouseOverCard();
            if (temporalCard == null)
            {
                if (mouseOverCard != null)
                {
                    mouseOverCard.MouseOverOut();
                    mouseOverCard = temporalCard;
                }
            }
            else
            {
                if (mouseOverCard == null)
                {
                    mouseOverCard = temporalCard;
                    temporalCard.MouseOverIn();
                    return;
                }
                if (mouseOverCard != temporalCard)
                {
                    mouseOverCard.MouseOverOut();
                    temporalCard.MouseOverIn();
                    mouseOverCard = temporalCard;
                }
            }
        }

        public Card FindMouseOverCard()
        {
            List<Card> temporalList = new List<Card>();
            foreach (Card card in hand.cards)
            {
                if (card.CheckMouseOver(GetMouseWorldPos(card.Position.z)))
                {
                    temporalList.Add(card);
                }
            }
            if (temporalList.Count == 0)
            {
                return null;
            }
            Card mouseOverCard = null;
            int sortOrder = -1;
            foreach (Card tempCard in temporalList)
            {
                if (tempCard.GetComponent<MeshRenderer>().sortingOrder > sortOrder)
                {
                    sortOrder = tempCard.GetComponent<MeshRenderer>().sortingOrder;
                    mouseOverCard = tempCard;
                }
            }
            return mouseOverCard;
        }

        public override void DragCheck()
        {
            if (Mouse.current.leftButton.wasPressedThisFrame & mouseOverCard != null)
            {
                draggedCard = mouseOverCard;
                draggedCard.OnDragStart();
                isDragging = true;
                dragManager.SetIsDragging(true);
            }
            if (Mouse.current.leftButton.wasReleasedThisFrame & draggedCard != null)
            {
                draggedCard.OnDragEnd();
                draggedCard = null;
                isDragging = false;
                dragManager.SetIsDragging(false);
                if (potentialCell != null)
                {
                    potentialCell.Fade();
                }
            }
        }

        public void CellCheck()
        {
            Cell closestCell = board.GetClosestCell(GetMouseWorldPos(-4.5f));
            //Debug.Log(closestCell.GetCoords()[1]);
            if (!draggedCard.CheckClosestCell(closestCell))
            {
                if(potentialCell != null)
                {
                    potentialCell.Fade();
                }
                potentialCell = null;
                return;
            }
            if (potentialCell != closestCell)
            {
                if (potentialCell != null)
                {
                    potentialCell.Fade();
                }
                potentialCell = closestCell;
                closestCell.IlluminateTexture(draggedCard.GetComponent<MeshRenderer>().material.mainTexture);
            }
        }
    }
}