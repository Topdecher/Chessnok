using Assets.Scripts.BoardEnvironment;
using Assets.Scripts.States.CardStates;
using Assets.Scripts.DragHandlers;
using Assets.Scripts.EventSystem;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Pieces;
using Assets.Scripts.Pieces.Abilities.Keywords;
using Assets.Scripts.PlayerEnvironment;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Card : CardLikeObject, IMouseListener, IEventListener
    {
        public enum Place
        {
            Board,
            Hand, 
            Deck,
            Graveyard
        }

        protected const float MAX_DISTANCE = 0.8f;

        protected EventControl eventControl;

        protected Graveyard graveyard;
        protected Board board;
        protected Hand hand;
        protected Deck deck;
        protected HandDragHandler dragHandler;

        protected List<Ability> abilities;
        protected List<Keyword> keywords;
        protected List<Ability> listeningAbilities;

        protected string cardName;
        
        protected Place place;
        protected CardState cardState;

        protected Vector3 positionInHand;

        private void Update()
        {
            cardState.UpdateState();
            //if (IsDragged)
            //{
            //    OnDrag();
            //    return;
            //}

            //if (IsMouseOver)
            //{
            //    MouseOver();
            //}
        }

        private void FixedUpdate()
        {
            repositioner.MoveUpdate();
        }

        public virtual void ChangeState(System.Type stateType)
        {
            if (cardState != null)
            {
                Destroy(cardState.gameObject);
            }
            cardState = Instantiate(new GameObject()).AddComponent(stateType) as CardState;
            cardState.SetCard(this);
        }

        public virtual void MouseOver()
        {
            MouseOverTime += Time.deltaTime;
        }

        public virtual void MouseOverIn()
        {
            if (IsDragged)
            {
                return;
            }
            IsMouseOver = true;
            //Enlarge();
            MouseOverTime = 0f;
            Debug.Log("wtf card");
            //FaceDown();
        }

        public virtual void MouseOverOut()
        {
            if (IsDragged)
            {
                return;
            }
            IsMouseOver = false;
            //NormalizeSize();
            //FaceUp();
        }

        public virtual bool CheckMouseOver(Vector3 mouseCoords)
        {
            return IsPointIn(mouseCoords);
        }

        public bool IsMouseOver { get; set; }

        public float MouseOverTime { get; set; }

        public bool IsDragged { get; set; }

        //maybe useless
        public void OnAttachment()
        {
            Texture2D newTexture = Resources.Load<Texture2D>("Textures/" + cardName + "_texture");
            if (newTexture == null)
            {
                newTexture = Resources.Load<Texture2D>("Textures/BlackDimakCard_texture");
            }
            GetComponent<MeshRenderer>().material.mainTexture = newTexture;
            ActivateAnchor();
            repositioner = new Repositioner();
            repositioner.SetBaseParams(cardAnchor, 5f);
            NextPosition = Position;

            abilities = new List<Ability>();
            keywords = new List<Keyword>();
            listeningAbilities = new List<Ability>();

            InstanciateAbilities();
        }

        public void ActivateAnchor()
        {
            cardAnchor = new GameObject();
            cardAnchor.name = "Anchor";
            cardAnchor.transform.position = transform.position + new Vector3(0.25f, 184.5f/541f, 0f);
            transform.parent = cardAnchor.transform;
        }

        //DELETE
        public void SetName(string name) => cardName = name;

        public void OnDragStart()
        {
            MouseOverOut();
            IsDragged = true;
            repositioner.IsInstantReposition = true;
            //NormalizeSize();
        }

        public void OnDrag()
        {
            NextPosition = dragHandler.GetMouseWorldPos(Position.z);
            //Cell closestCell = dragHandler.potentialCell;
            //if (CheckClosestCell(closestCell))
            //{
            //    closestCell.IlluminateTexture(meshRenderer.material.mainTexture);
            //}
        }

        public bool CheckClosestCell(Cell cell)
        {
            Vector3 mousePos = dragHandler.GetMouseWorldPos(-4.5f);
            float distance = (cell.GetAnchorPosition() - mousePos).magnitude;
            return cell.IsEmpty() & distance < MAX_DISTANCE & cell.GetTeam() == 0;
        }

        public void OnDragEnd()
        {
            IsDragged = false;
            repositioner.IsInstantReposition = false;
            if (dragHandler.potentialCell != null)
            {
                PlayCard(dragHandler.potentialCell);
            }
            else
            {
                ReturnToHand();
            }
        }

        public void Enlarge()
        {
            //Vector3 enlargePoint = hand.EnlargePoint();
            //Vector3 enlargeDirection = enlargePoint - Position;
            //float ratioOfMultitude = 1 / Mathf.Abs(enlargeDirection.z);
            //NextPosition += enlargeDirection / 4f;
            Vector3 enlargeVec = Vector3.up/10f;
            InstantMove(Position + enlargeVec);
            Resize(1.5f);
            SetSortingLayer("EnlargedCards");
        }

        public void NormalizeSize()
        {
            //NextPosition = positionInHand;
            InstantMove(positionInHand);
            Resize(1f / 1.5f);
            SetSortingLayer("Hand");
        }

        public void ReturnToHand()
        {
            hand.Rearrange();
        }

        public void SetHand(Hand hand) => this.hand = hand;
        public Hand GetHand() => hand;

        public void SetBoard(Board board) => this.board = board;
        public Board GetBoard() => board;

        public void SetDragHandler(HandDragHandler dragHandler) => this.dragHandler = dragHandler;

        public bool IsDeaf { get; set; }

        public virtual void Listen(GameEvent gameEvent) 
        {
            if (IsDeaf)
            {
                return;
            }
        }

        public virtual void PlayCard(Cell cell) { }

        public virtual void CheckAbilityType() { }

        public virtual void InstanciateAbilities() { }

        public EventControl GetEventControl() => eventControl;

        public void SetEventControl(EventControl eventControl) => this.eventControl = eventControl;

        public void SetHandPosition(Vector3 positionInHand) => this.positionInHand = positionInHand;
    }
}