using Assets.Scripts.EventSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Interfaces
{
    interface IEventListener
    {
        public bool IsDeaf { get; set; }

        public void Listen(GameEvent gameEvent);
    }
}
