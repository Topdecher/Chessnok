﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Interfaces
{
    interface IDraggable
    {
        public bool IsDraggable();

        public void OnDrag();

        public void OnDragStart();

        public void OnDragEnd();
    }
}
