using UnityEngine;

public class MeshScript : MonoBehaviour
{
    void Start()
    {
        MeshFilter filter = GetComponent<MeshFilter>();
        MeshCollider collider = GetComponent<MeshCollider>();


        Mesh mesh = new Mesh();
        mesh.vertices = new Vector3[] { Vector3.up, Vector3.right, Vector3.down, Vector3.left, Vector3.forward, Vector3.back };
        mesh.uv = new Vector2[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1) };
        mesh.triangles = new int[] { 0, 4, 1, 1, 5, 0, 5, 3, 0, 3, 4, 0 };

        filter.mesh = collider.sharedMesh = mesh;
    }
}