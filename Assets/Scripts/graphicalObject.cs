using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class GraphicalObject : MonoBehaviour {

    public int xSize = 5, ySize = 5;

    private Mesh mesh;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();

        // Constructs a list of vertice locations and colours and assigns it to the mesh
        mesh.vertices = ConstructVecticeArray();

        mesh.colors = new Color[mesh.vertices.Length];
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            mesh.colors[i] = new Color(0.8f, 0.3f, 1.0f);
        }

        // References the elements of vertices and colors to create a face
        mesh.triangles = ConstructTriangles(mesh.vertices.Length);
        mesh.RecalculateBounds();
    }

    Vector3[] ConstructVecticeArray()
    {
        List<Vector3>verts = new List<Vector3>();

        // Ensures that the mesh can be centered properly
        int iMax = int.Equals(xSize % 2, 0) ? Mathf.CeilToInt(xSize / 2) : Mathf.CeilToInt(xSize / 2) + 1;
        int jMax = int.Equals(ySize % 2, 0) ? Mathf.CeilToInt(xSize / 2) : Mathf.CeilToInt(ySize / 2) + 1;

        for (int i = -Mathf.CeilToInt(xSize / 2); i < iMax; i++)
        {
            for(int j = -Mathf.CeilToInt(ySize / 2); j < jMax; j++)
            {
                verts.Add(new Vector3(i, j, 0));
            }
        }

        return verts.ToArray();
    }

    // Creates faces based on the information in mesh.vertices
    int[] ConstructTriangles(int vertArrayLen)
    {
        List<int> tris = new List<int>();

        for(int i = 0, j = ySize; i < vertArrayLen - 1 && j < vertArrayLen - 1; i++, j++)
        {
            if((i + 1) % ySize != 0)
            {
                tris.Add(i);
                tris.Add(i + 1);
                tris.Add(i + ySize);

                tris.Add(i + 1);
                tris.Add(j + 1);
                tris.Add(j);
            }
        }

        return tris.ToArray();
    }
}
