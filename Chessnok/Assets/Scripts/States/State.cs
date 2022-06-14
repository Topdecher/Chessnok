using System.Collections;
using UnityEngine;

namespace Assets.Scripts.States
{
    public class State : MonoBehaviour
    {
        public virtual void UpdateState() { }

        public virtual void StartState() { }

        public virtual void EndState() { }

        public virtual void SelfDestroy()
        {
            Destroy(gameObject);
        }
    }
}