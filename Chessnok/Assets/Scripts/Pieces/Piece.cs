using Assets.Scripts.BoardEnvironment;
using Assets.Scripts.States.CardStates;
using Assets.Scripts.EventSystem;
using Assets.Scripts.Pieces.Abilities.Keywords;
using Assets.Scripts.Pieces.Abilities.OnPlayAbilities;
using Assets.Scripts.Pieces.Abilities.PieceTypeAbilities;
using Assets.Scripts.Pieces.PieceActions;
using Assets.Scripts.Pieces.Stats;
using Assets.Scripts.Pieces.Tokens;
using Assets.Scripts.Util.Meshes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Pieces
{
    public class Piece : Card
    {
        public enum PieceType
        {
            Pawn,
            Knight,
            Bishop,
            Rook,
            Queen,
            King
        }

        protected string artist;
        protected int rarity;
        public PieceType type;

        protected Stat power;

        protected Movement movement;
        protected AttackAction attackAction;

        protected Token mainToken;

        public int GetPower() => power.GetValue();
        public void SetPower(int powerValue) => power.SetValue(powerValue);
        public void ChangePower(int changeValue) => power.SetValue(power.GetValue() + changeValue);

        public virtual void SetMovement(Movement movement) => this.movement = movement;

        public virtual void SetAttackAction(AttackAction attackAction) => this.attackAction = attackAction;

        public override void PlayCard(Cell cell)
        {
            //InstatntiateBoardPiece(cell);
            hand.RemoveCard(this);
            foreach (Ability pieceTypeAbility in abilities)
            {
                if (pieceTypeAbility.GetType().BaseType == typeof(PieceTypeAbility))
                {
                    if (((PieceTypeAbility)pieceTypeAbility).GetRealType() == typeof(OnPlayAbility))
                    {
                        pieceTypeAbility.ThrowEvent();
                    }
                }
            }
            foreach (Ability onPlayAbility in abilities)
            {
                if (onPlayAbility.GetType() == typeof(OnPlayAbility))
                {
                    onPlayAbility.ThrowEvent();
                }
            }
            MoveToCell(cell);
            ChangeState(typeof(BoardState));
            //Disable();
        }


        public override void Listen(GameEvent gameEvent)
        {
            base.Listen(gameEvent);
            foreach (Ability ability in listeningAbilities)
            {
                ability.Listen(gameEvent);
            }
        }

        //board piece creation logic
        public virtual void InstatntiateBoardPiece(Cell cell)
        {
            BoardPiece piece = InstantiatePieceObject();
            SetPieceAnchor(piece);
            SetBaseParams(piece);
            InstantiateStatTrackers(piece);
            MoveToCell(cell, piece);
            cell.PlacePiece(piece);
        }
        
        public BoardPiece InstantiatePieceObject() 
        {
            BoardPiece boardPiece = Instantiate(new GameObject()).AddComponent<BoardPiece>();
            boardPiece.transform.position = new Vector3(0f, 0f, 0f);
            boardPiece.gameObject.AddComponent<CardMesh>();
            boardPiece.gameObject.GetComponent<CardMesh>().ActivateComponents();
            Texture newTexture = GetComponent<MeshRenderer>().material.mainTexture;
            boardPiece.gameObject.GetComponent<MeshRenderer>().material.mainTexture = newTexture;
            boardPiece.gameObject.GetComponent<MeshRenderer>().sortingLayerName = "Board";
            return boardPiece;
        }

        public void SetPieceAnchor(BoardPiece piece) 
        {
            GameObject pieceAnchor = Instantiate(new GameObject(), new Vector3(0, 0, -4.5f), Quaternion.identity);
            pieceAnchor.name = "BoardPieceAnchor";
            piece.gameObject.transform.parent = pieceAnchor.transform;
            piece.SetAnchor(pieceAnchor);
            Vector3 scale = piece.gameObject.transform.lossyScale;
            piece.gameObject.transform.localPosition -= new Vector3(scale.x / 2f, scale.y / 2f, 0f);
        }

        public void SetBaseParams(BoardPiece piece) 
        {
            piece.SetPiece(this);
            piece.SetBaseActions(attackAction, movement);
            piece.SetBaseAbilities(abilities, keywords);
            piece.SetBasePower(power);
        }

        public void InstantiateStatTrackers(BoardPiece piece) { }

        public void MoveToCell(Cell cell, BoardPiece piece)
        {
            piece.SetBoard(cell.board, cell);
            piece.InstantMove(cell.GetAnchorPosition());
        }

        public void MoveToCell(Cell cell)
        {
            SetBoard(cell.board);
            cell.PlacePiece(this);
            InstantMove(cell.GetAnchorPosition());
            Resize(1.6f);
            meshRenderer.sortingLayerName = "BoardPieces";
        }

        public PieceType GetPieceType() => type;
    }
}