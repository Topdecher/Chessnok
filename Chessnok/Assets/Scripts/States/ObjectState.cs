using Assets.Scripts.Interfaces;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.States
{
    public class ObjectState : State, IMouseListener
    {
        public virtual bool IsMouseOver { get; set; }
        public virtual float MouseOverTime { get; set; }

        public virtual bool CheckMouseOver(Vector3 mouseCoords)
        {
            return false;
        }

        public virtual void MouseOver()
        {
        }

        public virtual void MouseOverIn()
        {
        }

        public virtual void MouseOverOut()
        {

        }


    }
}