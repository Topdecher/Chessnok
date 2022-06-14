using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    interface IMouseListener
    {
        public bool IsMouseOver { get; set; }

        public float MouseOverTime { get; set; }

        public void MouseOver();

        public void MouseOverIn();

        public void MouseOverOut();

        public bool CheckMouseOver(Vector3 mouseCoords);

    }
}
