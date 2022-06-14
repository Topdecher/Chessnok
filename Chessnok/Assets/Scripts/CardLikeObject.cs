using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class CardLikeObject : TeamObject
    {
        protected const float BASE_HEIGHT = 738f / 541f;
        protected const float BASE_WIDTH = 1f;
        protected int baseSortingOrder;
        protected GameObject cardAnchor;
        protected Repositioner repositioner;

        public Vector3 Position
        {
            get => cardAnchor.transform.position;
            set => cardAnchor.transform.position = value;
        }

        public Vector3 NextPosition
        {
            get => repositioner.NextPosition;
            set => repositioner.NextPosition = value;
        }

        public Vector3 GetAnchorPosition()
        {
            if (cardAnchor != null)
            {
                return cardAnchor.transform.position;
            }
            else
            {
                return transform.position;
            }
        }

        public void SetAnchor(GameObject anchor) => cardAnchor = anchor;

        public bool IsPointIn(Vector3 point)
        {
            Vector3 center = GetAnchorPosition();
            float xOffset = Math.Abs(center.x - point.x);
            float yOffset = Math.Abs(center.y - point.y);

            float currentWidth = transform.lossyScale.x / 2f;
            float currentHeight = transform.lossyScale.y / 2f;
            //Debug.Log(xOffset < 0.25f & yOffset < 0.34f);
            return xOffset < currentWidth & yOffset < currentHeight;
        }

        public void MoveTo(Vector3 newPosition)
        {
            repositioner.NextPosition = newPosition;
        }

        public void MoveTo(Vector3 newPosition, float temporalSpeed, float time)
        {
            repositioner.NextPosition = newPosition;
            repositioner.SetTemporalSpeed(temporalSpeed, time);
        }

        public void MoveTo(Vector3 newPosition, float withThisSpeed)
        {
            Vector3 moveVector = newPosition - transform.position;
            float time = moveVector.magnitude / withThisSpeed;
            MoveTo(newPosition, withThisSpeed, time);
        }

        public void InstantMove(Vector3 newPos)
        {
            Position = newPos;
            NextPosition = newPos;
        }

        public void FaceUp()
        {
            cardAnchor.transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }

        public void FaceDown()
        {
            cardAnchor.transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        public void Resize(float scale)
        {
            float newX;
            float newY;
            if (cardAnchor != null)
            {
                newX = cardAnchor.transform.lossyScale.x * scale;
                newY = cardAnchor.transform.lossyScale.y * scale;
                cardAnchor.transform.localScale = new Vector3(newX, newY, 1f);
            }
            else
            {
                newX = transform.localScale.x * scale;
                newY = transform.localScale.y * scale;
                transform.localScale = new Vector3(newX, newY, 1f);
            }
        }

        public void ResizeToBase()
        {
            if (cardAnchor != null)
            {
                cardAnchor.transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }

        public void SetSortingOrder(int order)
        {
            meshRenderer.sortingOrder = order;
        }

        public void ToBaseSortingOrder()
        {
            meshRenderer.sortingOrder = baseSortingOrder;
        }

        public void SetBaseSortingOrder(int order)
        {
            baseSortingOrder = order;
        }

        public void SetSortingLayer(string sortingLayer)
        {
            meshRenderer.sortingLayerName = sortingLayer;
        }
    }
}