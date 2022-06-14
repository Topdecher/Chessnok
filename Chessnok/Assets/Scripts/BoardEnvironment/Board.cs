using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.BoardEnvironment
{
    public class Board : MonoBehaviour
    {
        private const float CELL_WIDTH = 3.526f / 4f;
        private const float CELL_HEIGHT = 4.84f / 4f;
        private Cell[,] cells = new Cell[4, 4];
        public List<BoardPiece> pieces;
        public GameObject[] boardHalves = new GameObject[2];
        public Cell cellPrefab;

        public void Start()
        {
            InstantiateCells();
        }

        public void InstantiateCells()
        {
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    Cell newCell = Instantiate(cellPrefab);
                    newCell.board = this;
                    newCell.SetCoords(new int[2] { x, y });
                    newCell.GoToPosition();
                    newCell.Fade();
                    cells[x, y] = newCell;

                    if (y < 2)
                    {
                        newCell.SetTeam(0);
                    }
                    else
                    {
                        newCell.SetTeam(1);
                    }
                }
            }
        }

        public Cell GetCell(int x, int y) => cells[x, y];

        public Cell GetClosestCell(Vector3 objPoint)
        {
            Vector3 relativePoint = objPoint - transform.position;
            float xGap = relativePoint.x / CELL_WIDTH;
            float yGap = relativePoint.y / CELL_HEIGHT;
            int xGapNum, yGapNum;
            xGapNum = Mathf.FloorToInt(xGap) + 2;
            if (xGapNum < 0)
            {
                xGapNum = 0;
            }
            else if (xGapNum > 3)
            {
                xGapNum = 3;
            }
            yGapNum = Mathf.FloorToInt(yGap) + 2;
            if (yGapNum < 0)
            {
                yGapNum = 0;
            }
            else if (yGapNum > 3)
            {
                yGapNum = 3;
            }
            return cells[xGapNum, yGapNum];
        }

        public List<Cell> GetCellsForPlace(int team)
        {
            List<Cell> emptyCells = new List<Cell>();
            for (int y = team * 2; y < team * 2 + 2; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    Cell currentCell = cells[x, y];
                    if (currentCell.IsEmpty())
                    {
                        emptyCells.Add(currentCell);
                    }
                }
            }
            return emptyCells;
        }
    }
}