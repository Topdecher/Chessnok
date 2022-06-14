using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    public class MouseHandler : MonoBehaviour
    {
        public enum Side
        {
            Left,
            Right
        }

        private Vector2 mouseScreenPos;
        private Camera playerCamera;

        private bool leftButtonPressed;
        private bool leftButtonReleased;

        private bool rightButtonPressed;
        private bool rightButtonReleased;

        public void UpdateMouseManager()
        {
            UpdateMousePos();
            UpdateButtons();
        }

        public void UpdateMousePos()
        {
            mouseScreenPos = Mouse.current.position.ReadValue();
        }

        public void UpdateButtons()
        {
            leftButtonPressed = Mouse.current.leftButton.wasPressedThisFrame;
            leftButtonReleased = Mouse.current.leftButton.wasReleasedThisFrame;

            rightButtonPressed = Mouse.current.rightButton.wasPressedThisFrame;
            rightButtonReleased = Mouse.current.rightButton.wasReleasedThisFrame;
        }

        public Vector3 GetMouseWorldPos(float depth)
        {
            Vector3 screenPos = new Vector3(mouseScreenPos.x, mouseScreenPos.y, depth + 10f);
            return playerCamera.ScreenToWorldPoint(screenPos);
        }

        public Vector2 GetMousePos() => mouseScreenPos;

        public bool IsButtonPressed(Side side)
        {
            if (side == Side.Left)
            {
                return leftButtonPressed;
            }
            else
            {
                return rightButtonPressed;
            }
        }

        public bool IsButtonReleased(Side side)
        {
            if (side == Side.Left)
            {
                return leftButtonReleased;
            }
            else
            {
                return rightButtonReleased;
            }
        }
    }
}