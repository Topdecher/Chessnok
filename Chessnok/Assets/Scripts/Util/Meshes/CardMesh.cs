using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Util.Meshes
{
    public class CardMesh : MonoBehaviour
    {
        // modified quad code
        // imma freaking genious
        public float width = 1;
        public float height = 1;

        public void Start()
        {
            ActivateComponents();
            MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
            MeshCollider meshCollider = gameObject.GetComponent<MeshCollider>();

            Mesh mesh = new Mesh();

            Vector3[] vertices = new Vector3[8]
            {
            new Vector3(width, 0, 0),
            new Vector3(0, 0, 0),
            new Vector3(width, height, 0),
            new Vector3(0, height, 0),
            new Vector3(0, 0, 0),
            new Vector3(width, 0, 0),
            new Vector3(0, height, 0),
            new Vector3(width, height, 0)
            };
            mesh.vertices = vertices;

            int[] tris = new int[12]
            {
            // lower left triangle
            1, 2, 0,
            // upper right triangle
            2, 1, 3,
            // reversed for card back
            // don't give a shit how this works
            5, 6, 4,
            5, 7, 6
            };
            mesh.triangles = tris;

            Vector3[] normals = new Vector3[8]
            {
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
            // reversed for card back
            // actually do nothing either forward and back
            // idk why
            -Vector3.back,
            -Vector3.back,
            -Vector3.back,
            -Vector3.back
            };
            mesh.normals = normals;

            Vector2[] uv = new Vector2[8]
            {
            // texture will be spliced in 2 parts - front and back
            new Vector2(0.5f, 0f),
            new Vector2(0, 0),
            new Vector2(0.5f, 1),
            new Vector2(0, 1),
            new Vector2(1, 0),
            new Vector2(0.5f, 0),
            new Vector2(1, 1),
            new Vector2(0.5f, 1)
            
            };
            mesh.uv = uv;

            meshFilter.mesh = mesh;
            meshCollider.sharedMesh = mesh;
        }

        public void ActivateComponents()
        {
            if (gameObject.GetComponent<MeshFilter>() == null)
            {
                gameObject.AddComponent<MeshFilter>();
            }
            if (gameObject.GetComponent<MeshCollider>() == null)
            {
                gameObject.AddComponent<MeshCollider>();
            }
        }
    }  
}