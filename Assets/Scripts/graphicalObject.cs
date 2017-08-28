using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class GraphicalObject : MonoBehaviour {

    /*
    A third dimension(z) can easily be added to this code as part
    of our extra feature.
    */

    public float rotSpeed = 1;
    public int xSize = 5, ySize = 5;

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
        mesh.triangles = ConstructTriangles(mesh.vertices.Length);
    }

    Vector3[] ConstructVecticeArray()
    {
        List<Vector3>test = new List<Vector3>();
        int iMax = int.Equals(xSize % 2, 0) ? Mathf.CeilToInt(xSize / 2) : Mathf.CeilToInt(xSize / 2) + 1;
        int jMax = int.Equals(ySize % 2, 0) ? Mathf.CeilToInt(xSize / 2) : Mathf.CeilToInt(ySize / 2) + 1;

        for (int i = -Mathf.CeilToInt(xSize / 2); i < iMax; i++)
        {
            for(int j = -Mathf.CeilToInt(ySize / 2); j < jMax; j++)
            {
                test.Add(new Vector3(i, j, 0));

                //foreach (Vector3 element in test)
                //{
                //    Debug.Log(element);
                //}
            }
        }

        return test.ToArray();
    }

    // Creates spheres on each location that a vertice will created in scene
    private void OnDrawGizmos()
    {
        foreach(Vector3 element in ConstructVecticeArray())
        {
            Gizmos.DrawSphere(element, 0.1f);
        }
    }

    // Creates faces based on the information in mesh.vertices
    int[] ConstructTriangles(int vertArrayLen)
    {
        List<int> test = new List<int>();

        // Only works for Squares 
        for(int i = 0, j = ySize; i < vertArrayLen - 1 && j < vertArrayLen - 1; i++, j++)
        {
            if((i + 1) % ySize != 0)
            {
                test.Add(i);
                test.Add(i + 1);
                test.Add(i + ySize);

                test.Add(i + 1);
                test.Add(j + 1);
                test.Add(j);
            }
        }

        return test.ToArray();
    }
}
