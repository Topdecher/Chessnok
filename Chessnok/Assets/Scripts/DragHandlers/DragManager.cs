using System.Collections;
using UnityEngine;

namespace Assets.Scripts.DragHandlers
{
    public class DragManager
    {
        private BoardDragHandler boardHandler;
        private HandDragHandler handHandler;

        private bool isDragging;

        public bool GetIsDragging() => isDragging;

        public void SetIsDragging(bool isDragging) => this.isDragging = isDragging;
    }
}