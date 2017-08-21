using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class graphicalObject : MonoBehaviour {

    public float rotSpeed;
    public int xSize, ySize;
    //public Color[] objectCols;

    private Mesh mesh;

    void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();

        mesh.vertices = new Vector3[]
        {
            new Vector3(0, 0, 0),
            new Vector3(0, 1, 0),
            new Vector3(1, 1, 0),
            new Vector3(1, 0, 0)
        };

        mesh.colors = new Color[]
        {
            new Color(0.8f, 0.3f, 1.0f),
            new Color(0.8f, 0.3f, 1.0f),
            new Color(0.8f, 0.3f, 1.0f),
            new Color(0.8f, 0.3f, 1.0f)
        };

        // References the elements of vertices and colors to create a face
        mesh.triangles = new int[] { 0, 1, 2, 0, 2, 3 };
        mesh.triangles = new int[] { 0, 1, 2 };
    }

    Vector3[,] ConstructVecticeArray()
    {
        Vector3[,] test = new Vector3[xSize, ySize];

        for(int i = 0; i < xSize; i++)
        {
            for(int j = 0; j < ySize; j++)
            {
                test[i, j] = new Vector3(i * 1, j * 1, 0);
                // Debug.Log("Element " + i + j + " = " + test[i, j]);
            }
        }

        return test;
    }

    private void OnDrawGizmos()
    {
        foreach(Vector3 element in ConstructVecticeArray())
        {
            Gizmos.DrawSphere(element, 0.1f);
        }
    }

    void ConstructTri()
    {

    }
}
