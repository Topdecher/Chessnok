using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Pieces.PieceActions
{
    public class Movement
    {
        public enum MoveDirection
        {
            DiagonalLeftUp,
            DiagonalLeftDown,
            DiagonalRightDown,
            DiagonalRightUp,
            StraightUp,
            StraightLeft,
            StraightDown,
            StraightRight,
            Horse,
        }

        public enum MoveType
        {
            Dash = 0,
            Jump = 1
        }

        public enum MoveListType
        {
            AttackList,
            NonAttackList
        }

        private List<int[]> moveList;
        private List<int[]> attackMoveList;

        public void AddMovement(MoveDirection direction, MoveListType moveListType, int range)
        {
            List<int[]> currentMoveList;
            if (moveListType == MoveListType.AttackList)
            {
                currentMoveList = attackMoveList;
            }
            else
            {
                currentMoveList = moveList;
            }

            //BAD CODE
            if (direction == MoveDirection.Horse)
            {
                int[] newMove1 = new int[4] { -1, 2, (int)MoveType.Jump, range };
                int[] newMove2 = new int[4] { 1, 2, (int)MoveType.Jump, range };

                int[] newMove3 = new int[4] { -2, 1, (int)MoveType.Jump, range };
                int[] newMove4 = new int[4] { -2, -1, (int)MoveType.Jump, range };

                int[] newMove5 = new int[4] { -1, -2, (int)MoveType.Jump, range };
                int[] newMove6 = new int[4] { -1, 2, (int)MoveType.Jump, range };

                int[] newMove7 = new int[4] { 2, -1, (int)MoveType.Jump, range };
                int[] newMove8 = new int[4] { 2, 1, (int)MoveType.Jump, range };
                
                currentMoveList.AddRange(new List<int[]> { newMove1, newMove2, newMove3, newMove4, 
                    newMove5, newMove6, newMove7, newMove8 });

                return;
            }

            int[] newMove = null;
            switch (direction)
            {
                case MoveDirection.DiagonalLeftUp:
                    newMove = new int[4] { -1, 1, (int)MoveType.Dash, range };
                    break;

                case MoveDirection.DiagonalLeftDown:
                    newMove = new int[4] { -1, -1, (int)MoveType.Dash, range };
                    break;

                case MoveDirection.DiagonalRightDown:
                    newMove = new int[4] { 1, -1, (int)MoveType.Dash, range };
                    break;

                case MoveDirection.DiagonalRightUp:
                    newMove = new int[4] { 1, 1, (int)MoveType.Dash, range };
                    break;

                case MoveDirection.StraightUp:
                    newMove = new int[4] { 0, 1, (int)MoveType.Dash, range };
                    break;

                case MoveDirection.StraightLeft:
                    newMove = new int[4] { -1, 0, (int)MoveType.Dash, range };
                    break;

                case MoveDirection.StraightDown:
                    newMove = new int[4] { 0, -1, (int)MoveType.Dash, range };
                    break;

                case MoveDirection.StraightRight:
                    newMove = new int[4] { 1, 0, (int)MoveType.Dash, range };
                    break;
            }
            if (newMove != null)
            {
                currentMoveList.Add(newMove);
            }
        }

        public void AddMovement(MoveDirection direction, MoveListType moveListType)
        {
            AddMovement(direction, moveListType, 6);
        }

        public void AddMovementToBothLists(MoveDirection moveDirection)
        {
            AddMovement(moveDirection, MoveListType.AttackList);
            AddMovement(moveDirection, MoveListType.NonAttackList);
        }

        public void AddMovementToBothLists(MoveDirection moveDirection, int range)
        {
            AddMovement(moveDirection, MoveListType.AttackList, range);
            AddMovement(moveDirection, MoveListType.NonAttackList, range);
        }

        public void AddCustomMovement(int[] customMove, MoveListType moveListType)
        {
            List<int[]> currentMoveList;
            if (moveListType == MoveListType.AttackList)
            {
                currentMoveList = attackMoveList;
            }
            else
            {
                currentMoveList = moveList;
            }

            if (!currentMoveList.Contains(customMove))
            {
                currentMoveList.Add(customMove);
            }
        }

        public void AddCustomMovement(List<int[]> customMoves, MoveListType moveListType)
        {
            List<int[]> currentMoveList;
            if (moveListType == MoveListType.AttackList)
            {
                currentMoveList = attackMoveList;
            }
            else
            {
                currentMoveList = moveList;
            }

            foreach (int[] customMove in customMoves)
            {
                if (!currentMoveList.Contains(customMove))
                {
                    currentMoveList.Add(customMove);
                }
            }
        }
    }
}