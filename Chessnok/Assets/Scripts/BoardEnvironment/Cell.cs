using System.Collections;
using UnityEngine;
using Assets.Scripts.Pieces;
using System.Collections.Generic;

namespace Assets.Scripts.BoardEnvironment
{
    public class Cell : CardLikeObject
    {
        public Board board;
        private Piece groundPiece;
        private BoardPiece flyingPiece;
        private List<BoardPiece> otherPieces;

        private BoardPiece _groundPiece;

        // board coords
        public int[] coords;

        public void SetCoords(int[] coords) => this.coords = coords;

        public void GoToPosition()
        {
            int x = coords[0];
            int y = coords[1];
            Vector3 boardAnchor = board.transform.position;
            GameObject boardHalf = board.boardHalves[0];
            float width = (boardHalf.transform.lossyScale.x - 0.16f)/ 4;
            float height = (boardHalf.transform.lossyScale.y * 2 - 0.16f) / 4;
            Vector3 newPos = new Vector3((x - 1.5f) * width, (y - 1.5f) * height, -4.5f);
            transform.position = newPos + boardAnchor;
            
            Vector3 scale = transform.lossyScale;
            Vector3 offset = new Vector3(scale.x / 2f, scale.y / 2f, 0f);
            transform.position -= offset;
        }

        private void Start()
        {
            groundPiece = null;
            Vector3 scale = transform.lossyScale;
            Vector3 offset = new Vector3(scale.x / 2f, scale.y / 2f, 0f);
            cardAnchor = Instantiate(new GameObject(), transform.position + offset, Quaternion.identity);
            cardAnchor.name = "CellAnchor";
            transform.parent = cardAnchor.transform;
            //GoToPosition();
        }

        public int[] GetCoords() => coords;

        public bool IsEmpty() => groundPiece == null;

        public void PlacePiece(BoardPiece piece) 
        {
            groundPiece = piece;
        }

        public void PlacePiece(Piece piece) => groundPiece = piece;

        public void DiscardPiece() => groundPiece = null;

        public void Fade()
        {
            meshRenderer.material.color = new Color(1f, 1f, 1f, 0f);
        }

        public void Fade(float alpha)
        {
            meshRenderer.material.color = new Color(1f, 1f, 1f, alpha);
        }

        public void IlluminateTexture(Texture texture)
        {
            meshRenderer.material.mainTexture = texture;
            Fade(0.5f);
        }
    }
}