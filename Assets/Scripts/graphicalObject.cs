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

        mesh.vertices = ConstructVecticeArray();

        mesh.colors = new Color[mesh.vertices.Length];
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            mesh.colors[i] = new Color(0.8f, 0.3f, 1.0f);
        }

        // References the elements of vertices and colors to create a face
        mesh.triangles = new int[] { 0, 1, 2, 0, 2, 3 };
        mesh.triangles = new int[] { 0, 1, 2 };
        mesh.triangles = ConstructTriangles(25);
    }

    Vector3[] ConstructVecticeArray()
    {
        List<Vector3>test = new List<Vector3>();

        for(int i = 0; i < xSize; i++)
        {
            for(int j = 0; j < ySize; j++)
            {
                test.Add(new Vector3(i * 1, j * 1, 0));

                //foreach (Vector3 element in test)
                //{
                //    Debug.Log(element);
                //}
            }
        }

        return test.ToArray();
    }

    private void OnDrawGizmos()
    {
        foreach(Vector3 element in ConstructVecticeArray())
        {
            Gizmos.DrawSphere(element, 0.1f);
        }
    }

    int[] ConstructTriangles(int vertArrayLen)
    {
        List<int> test = new List<int>();

        for(int i = 0, j = xSize; i < vertArrayLen - 1 && j < vertArrayLen - 1; i++, j++)
        {
            test.Add(i);
            test.Add(i + 1);
            test.Add(i + xSize);

            test.Add(i + 1);
            test.Add(j + 1);
            test.Add(j);
        }

        return test.ToArray();
    }
}
