using Assets.Scripts.PlayerEnvironment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.DragHandlers
{
    public class DragHandler : MonoBehaviour
    {
        private Vector2 mouseScreenPos;
        public Camera playerCamera;
        protected DragManager dragManager;
        protected bool isDragging;

        private void Start()
        {
        }

        private void Update()
        {

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

        public Vector2 GetMousePos() => mouseScreenPos;

        public virtual void MouseOverCheck() 
        {

        }

        public virtual void DragCheck() { }

        //implication
        public bool IsUpdatable() => !dragManager.GetIsDragging() || isDragging;
    }
}