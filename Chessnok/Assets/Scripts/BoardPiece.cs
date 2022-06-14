using Assets.Scripts.BoardEnvironment;
using Assets.Scripts.DragHandlers;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Pieces;
using Assets.Scripts.Pieces.Abilities.Keywords;
using Assets.Scripts.Pieces.PieceActions;
using Assets.Scripts.Pieces.Stats;
using Assets.Scripts.Pieces.Tokens;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class BoardPiece : Piece
    {
        public enum Fraction
        {
            White, Black,
        }

        //protected BoardDragHandler dragHandler;

        protected float timeToGetInformation;
        protected bool isGivingInformation;

        protected Piece piece;
        protected int id;
        protected Fraction fraction;
        
        protected Cell currentCell;

        //protected Stat power;

        protected List<Token> tokens;
        protected int tokenLimit;

        //protected AttackAction attackAction;
        //protected Movement movement;

        //protected List<Ability> abilities;
        //protected List<Keyword> keywords;

        //public bool IsMouseOver { get; set; }
        //public float MouseOverTime { get; set; }

        private void FixedUpdate()
        {
            repositioner.MoveUpdate();
        }

        private void Update()
        {
            
        }

        public override void MouseOver()
        {
            MouseOverTime += Time.deltaTime;
            if (MouseOverTime > timeToGetInformation)
            {
                if (!isGivingInformation)
                {
                    GetInformation();
                }
                isGivingInformation = true;
            }
        }

        public override void MouseOverIn()
        {
            IsMouseOver = true;
        }

        public override void MouseOverOut()
        {
            isGivingInformation = false;
            IsMouseOver = false;
            MouseOverTime = 0f;
        }

        public virtual void Move(Cell cell)
        {

        }

        public virtual void Attack(BoardPiece piece)
        {

        }

        public virtual void SetBaseActions(AttackAction attackAction, Movement movement) 
        {
            this.attackAction = attackAction;
            this.movement = movement;
        }

        public void SetBaseAbilities(List<Ability> abilities, List<Keyword> keywords)
        {
            this.abilities = abilities;
            this.keywords = keywords;
        }

        public void SetBasePower(Stat power) => this.power = power;

        public void SetBoard(Board board, Cell cell)
        {
            this.board = board;
            currentCell = cell;
        }

        public void SetPiece(Piece piece) => this.piece = piece;

        //for mouse over
        public virtual void GetInformation() { }


    }
}