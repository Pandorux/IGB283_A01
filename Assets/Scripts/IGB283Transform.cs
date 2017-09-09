using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class IGB283Transform : MonoBehaviour {

    public float rotSpeed = 1;
    public float xSpeed = 1, ySpeed = 1;
    public float xScaSpeed = 0.25f, yScaSpeed = 0.25f;
    public Quaternion initialScale = new Quaternion(1, 1, 1, 1);
    public Vector3 initialPosition = new Vector3(0, 0, 0);

    private Mesh mesh;

    void Start()
    {
        mesh = this.GetComponent<MeshFilter>().mesh;

        Matrix3x3 t = TransMatrix(initialPosition);
        Matrix3x3 s = ScaleMatrix(initialScale);
        Matrix3x3 m = t * s;
        Vector3[] verts = mesh.vertices;

        mesh.vertices = TranslateMesh(m, verts);
        mesh.RecalculateBounds();
        this.GetComponent<MeshRenderer>().material.shader = Shader.Find("Unlit/Color");
    }

    void Update()
    {
        Vector3 rotOrigin = this.GetComponent<MeshRenderer>().bounds.center;
        Matrix3x3 t = TransMatrix(new Vector3(xSpeed * Time.deltaTime, ySpeed * Time.deltaTime, 1));
        Matrix3x3 m = CalculateRotatation(rotOrigin, rotSpeed);
        m = m * t;

        mesh.vertices = TranslateMesh(m, mesh.vertices);
        mesh.RecalculateBounds();
    }

    public Matrix3x3 TransMatrix(Vector3 offset)
    {
        Matrix3x3 m = new Matrix3x3();

        m.SetRow(0, new Vector3(1.0f, 0.0f, offset.x));
        m.SetRow(1, new Vector3(0.0f, 1.0f, offset.y));
        m.SetRow(2, new Vector3(0.0f, 0.0f, 1.0f));

        return m;
    }

    public Matrix3x3 ScaleMatrix(Quaternion scale)
    {
        Matrix3x3 m = new Matrix3x3();

        m.SetRow(0, new Vector3(scale.x, 0.0f, 0.0f));
        m.SetRow(1, new Vector3(0.0f, scale.y, 0.0f));
        m.SetRow(2, new Vector3(0.0f, 0.0f, 1.0f));

        return m;
    }

    public Matrix3x3 RotMatrix(float angle)
    {

        Matrix3x3 m = new Matrix3x3();

        m.SetRow(0, new Vector3(Mathf.Cos(angle), -Mathf.Sin(angle), 0.0f));
        m.SetRow(1, new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0.0f));
        m.SetRow(2, new Vector3(0.0f, 0.0f, 1.0f));

        return m;
    }

    Matrix3x3 CalculateRotatation(Vector3 originRot, float angle)
    {
        Matrix3x3 originNeg = TransMatrix(-originRot);
        Matrix3x3 origin = TransMatrix(originRot);
        Matrix3x3 r = RotMatrix(rotSpeed * Time.deltaTime);
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
}
