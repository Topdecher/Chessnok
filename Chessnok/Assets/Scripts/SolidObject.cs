using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class SolidObject : MonoBehaviour
    {
        protected MeshRenderer meshRenderer;
        protected MeshFilter meshFilter;

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        protected void Awake()
        {
            if (GetComponent<MeshRenderer>() != null)
            {
                meshRenderer = GetComponent<MeshRenderer>();
            }
            if (GetComponent<MeshFilter>() != null)
            {
                meshFilter = GetComponent<MeshFilter>();
            }
        }
    }
}