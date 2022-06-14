using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class LolScript : MonoBehaviour
    {
        void Start()
        {
            GetComponent<MeshRenderer>().material.color = new Color(1, 0, 0, 0.5f);
            GetComponent<MeshRenderer>().sortingLayerName = "Board";
        }
    }
}