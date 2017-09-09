using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(ColourLerp))]
public class GraphicalObject : MonoBehaviour {

    public float rotSpeed = 1;
    public float xSpeed = 1, ySpeed = 1;
    public int xSize = 5, ySize = 5;
    public float xScalar = 0.25f, yScalar = 0.25f;
    public Color32 colour00 = new Color32(0, 0, 0, 255), colour01 = new Color32(255, 255, 255, 255);

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

        Matrix3x3 T = IGB283Transform.TransMatrix(initialPosition);
        Matrix3x3 S = IGB283Transform.ScaleMatrix(initialScale);
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
        this.GetComponent<MeshRenderer>().material.shader = Shader.Find("Unlit/Color");
    }

    void Update()
    {
        rotOrigin = this.GetComponent<MeshRenderer>().bounds.center;
        Matrix3x3 t = IGB283Transform.TransMatrix(new Vector3 (xSpeed * Time.deltaTime, ySpeed * Time.deltaTime, 1));
        Matrix3x3 m = Rotate(rotOrigin, rotSpeed);
        m = m * t;

        mesh.vertices = TranslateMesh(m, mesh.vertices);
        mesh.RecalculateBounds();
        UpdateColour(rotOrigin);
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

    Matrix3x3 Rotate(Vector3 originRot, float angle)
    {
        Matrix3x3 originNeg = IGB283Transform.TransMatrix(-rotOrigin);
        Matrix3x3 origin = IGB283Transform.TransMatrix(rotOrigin);
        Matrix3x3 r = IGB283Transform.RotMatrix(rotSpeed * Time.deltaTime);
        Matrix3x3 m = origin * r * originNeg;
        return m;
    }

    Vector3[] TranslateMesh(Matrix3x3 trans, Vector3[] verts)
    {

        for (int i = 0; i < verts.Length; i++)
        {
            Vector3 v = trans.MultiplyPoint(verts[i]);
            ChangeDirection(v.x, v.y);
            verts[i].x = v.x;
            verts[i].y = v.y;
        }

        return verts;
    }

    void ChangeDirection(float xLoc, float yLoc)
    {
        if (xLoc >= 1)
        {
            xSpeed = -Mathf.Abs(xSpeed);
        }
        else if (xLoc <= -1)
        {
            xSpeed = Mathf.Abs(xSpeed);
        }

        if (yLoc >= 1)
        {
            ySpeed = -Mathf.Abs(ySpeed);
        }
        else if (yLoc <= -1)
        {
            ySpeed = Mathf.Abs(ySpeed);
        }
    }

    void UpdateColour(Vector3 meshCentre)
    {
        Color32 col = Color32.Lerp(colour00, colour01, Mathf.Clamp(meshCentre.x, -0.5f, 0.5f) * 2);
        this.GetComponent<MeshRenderer>().material.color = col;
    }
}
