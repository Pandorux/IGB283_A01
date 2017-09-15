using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IGB283Transform : MonoBehaviour {

    public float rotSpeed = 1;
    public float xSpeed = 1, ySpeed = 0;
    public Vector3 initialScale = new Vector3(0.1f, 0.1f, 1);
    public Vector3 initialPosition = new Vector3(0, 0, 0);

    private Mesh mesh;

    void Start()
    {
        mesh = this.GetComponent<MeshFilter>().mesh;

        // Sets the initial pos and scale of mesh.
        Matrix3x3 t = TransMatrix(initialPosition);
        Matrix3x3 s = ScaleMatrix(initialScale);
        Matrix3x3 m = t * s;
        Vector3[] verts = mesh.vertices;

        mesh.vertices = TranslateMesh(m, verts);
        mesh.RecalculateBounds();
    }

    void Update()
    {

        Vector3 rotOrigin = this.GetComponent<MeshRenderer>().bounds.center;
        Matrix3x3 t = TransMatrix(new Vector3(xSpeed * Time.deltaTime, ySpeed, 0));
        Matrix3x3 r = CalculateRotation(rotOrigin, rotSpeed);

        mesh.vertices = TranslateMesh(r, mesh.vertices);
        mesh.vertices = TranslateMesh(t, mesh.vertices);

        mesh.RecalculateBounds();
        ySpeed = 0;
    }

    // Creates a Translational Matrix.
    public Matrix3x3 TransMatrix(Vector3 offset)
    {
        Matrix3x3 m = new Matrix3x3();

        m.SetRow(0, new Vector3(1.0f, 0.0f, offset.x));
        m.SetRow(1, new Vector3(0.0f, 1.0f, offset.y));
        m.SetRow(2, new Vector3(0.0f, 0.0f, 1.0f));

        return m;
    }

    // Creates a Scale Matrix.
    public Matrix3x3 ScaleMatrix(Vector3 scale)
    {
        Matrix3x3 m = new Matrix3x3();

        m.SetRow(0, new Vector3(scale.x, 0.0f, 0.0f));
        m.SetRow(1, new Vector3(0.0f, scale.y, 0.0f));
        m.SetRow(2, new Vector3(0.0f, 0.0f, 1.0f));

        return m;
    }

    // Creates a Rotation Matrix.
    public Matrix3x3 RotMatrix(float angle)
    {

        Matrix3x3 m = new Matrix3x3();

        m.SetRow(0, new Vector3(Mathf.Cos(angle), -Mathf.Sin(angle), 0.0f));
        m.SetRow(1, new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0.0f));
        m.SetRow(2, new Vector3(0.0f, 0.0f, 1.0f));

        return m;
    }

    // Uses T-1, R, T to calculate the position of the matrix.
    Matrix3x3 CalculateRotation(Vector3 originRot, float angle)
    {
        Matrix3x3 originNeg = TransMatrix(-originRot);
        Matrix3x3 origin = TransMatrix(originRot);
        Matrix3x3 r = RotMatrix(rotSpeed * Time.deltaTime);
        Matrix3x3 m = origin * r * originNeg;
        return m;
    }

    // Updates the position of each vert within the mesh.
    Vector3[] TranslateMesh(Matrix3x3 trans, Vector3[] verts)
    {
        for (int i = 0; i < verts.Length; i++)
        {
            Vector3 v = trans.MultiplyPoint(verts[i]);

            verts[i].y = v.y;
            verts[i].x = v.x;

            ChangeDirection(v.x, v.y);
        }

        return verts;
    }

    // Ensures that the mesh stays approximately within the positional bounds.
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

    }
}
