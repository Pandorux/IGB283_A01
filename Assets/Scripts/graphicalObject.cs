using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(ColourLerp))]
public class GraphicalObject : MonoBehaviour {

    public int xSize = 5, ySize = 5;

    private Mesh mesh;
    private Vector3 rotOrigin;

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
    }

    Vector3[] ConstructVecticeArray()
    {
        List<Vector3>test = new List<Vector3>();

        // Ensures that the mesh can be centered properly
        int iMax = int.Equals(xSize % 2, 0) ? Mathf.CeilToInt(xSize / 2) : Mathf.CeilToInt(xSize / 2) + 1;
        int jMax = int.Equals(ySize % 2, 0) ? Mathf.CeilToInt(xSize / 2) : Mathf.CeilToInt(ySize / 2) + 1;

        for (int i = -Mathf.CeilToInt(xSize / 2); i < iMax; i++)
        {
            for(int j = -Mathf.CeilToInt(ySize / 2); j < jMax; j++)
            {
                test.Add(new Vector3(i, j, 0));

                // Debug.Log used to check a vert's location
                //foreach (Vector3 element in test)
                //{
                //    Debug.Log(element);
                //}
            }
        }

        return test.ToArray();
    }

    /* Following code is no longer needed except for debugging purposes

        // Creates spheres on each location that a vertice will created in scene
        //private void OnDrawGizmos()
        //{
        //    foreach(Vector3 element in ConstructVecticeArray())
        //    {
        //        Gizmos.DrawSphere(element, 0.1f);
        //    }
        //}

    */

    // Creates faces based on the information in mesh.vertices
    int[] ConstructTriangles(int vertArrayLen)
    {
        List<int> test = new List<int>();

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
