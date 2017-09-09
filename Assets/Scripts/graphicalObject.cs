using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(ColourLerp))]
public class GraphicalObject : MonoBehaviour {

    /*
    A third dimension(z) can easily be added to this code as part
    of our extra feature.
    */

    public float rotSpeed = 1;
    public float xSpeed = 1, ySpeed = 1;
    public int xSize = 5, ySize = 5;
    public float xScalar = 0.25f, yScalar = 0.25f;
    public Color32 colour00 = new Color32(0, 0, 0, 1), colour01 = new Color32(255, 255, 255, 1);

    public Quaternion initialScale = new Quaternion (1, 1, 1, 1);
    public Vector3 initialPosition = new Vector3(0, 0, 0);

    private Mesh mesh;
    private Vector3 rotOrigin;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();

        mesh.vertices = ConstructVecticeArray();

        mesh.colors = new Color[mesh.vertices.Length];
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            mesh.colors[i] = new Color(0.8f, 0.3f, 1.0f);
        }

        // References the elements of vertices and colors to create a face
        mesh.triangles = ConstructTriangles(mesh.vertices.Length);

        Matrix3x3 T = IGB283Transform.Translate(initialPosition);
        Matrix3x3 S = IGB283Transform.Scale(initialScale);
        Matrix3x3 M = T * S;
        Vector3[] verts = mesh.vertices;

        for(int i = 0; i < verts.Length; i++)
        {
            Vector3 v = M.MultiplyPoint(verts[i]);

            verts[i].x = v.x;
            verts[i].y = v.y;
        }

        mesh.vertices = verts;
        mesh.RecalculateBounds();
    }

    void Update()
    {
        rotOrigin = this.GetComponent<MeshRenderer>().bounds.center;
        Matrix3x3 T = IGB283Transform.Translate(new Vector3 (xSpeed * Time.deltaTime, ySpeed * Time.deltaTime, 1));
        Matrix3x3 T2 = IGB283Transform.Translate(-rotOrigin);
        Matrix3x3 T3 = IGB283Transform.Translate(rotOrigin);
        Matrix3x3 R = IGB283Transform.Rotate(rotSpeed * Time.deltaTime);
        Matrix3x3 M = T3 * R * T2;
        M = M * T;

        Vector3[] verts = mesh.vertices;
        Color32[] cols = mesh.colors32;

        for (int i = 0; i < verts.Length; i++)
        {
            Vector3 v = M.MultiplyPoint(verts[i]);

            Color32 vertCol = Color32.Lerp(colour00, colour01, v.x);
            cols[i] = vertCol;

            verts[i].x = v.x;
            verts[i].y = v.y;

            if (verts[i].x >= 1)
            {
                xSpeed = -Mathf.Abs(xSpeed);
            }
            else if(verts[i].x <= -1)
            {
                xSpeed = Mathf.Abs(xSpeed);
            }

            if (verts[i].y >= 1)
            {
                ySpeed = -Mathf.Abs(ySpeed);
            }
            else if (verts[i].y <= -1)
            {
                ySpeed = Mathf.Abs(ySpeed);
            }
        }

        mesh.vertices = verts;
        mesh.RecalculateBounds();

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

    Matrix3x3[] ConstructTriangleMatrixArray(Mesh mesha)
    {
        Matrix3x3[] m = new Matrix3x3[(int)mesha.triangles.Length / 3];

        for(int i = 0; i < m.Length; i++)
        {
            m[i] = new Matrix3x3(mesha.vertices[mesha.triangles[(i * 3)]], mesha.vertices[mesha.triangles[(i * 3)+1]], mesha.vertices[mesha.triangles[(i * 3) + 2]]);
            //Debug.Log("Total Verts: " + mesha.vertices.Length);
            //Debug.Log("Total Tris: " + mesha.triangles.Length);
            //Debug.Log("Total matrix" + m.Length);
            //Debug.Log("Tri " + i + ": Vert{" + (i * 3) + "}, Vert{" + ((i * 3) + 1) + "}, Vert{" + ((i * 3) + 2) + "}");
        }

        return m;
    }
}
