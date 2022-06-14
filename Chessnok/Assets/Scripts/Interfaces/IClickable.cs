using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Interfaces
{
    interface IClickable
    {
        public bool IsClickable();

        public void OnClick();

        public void DiscardClick();
    }
}
