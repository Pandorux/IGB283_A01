using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IGB283Transform : MonoBehaviour {

    public float movYSpeed = 1, movXSpeed = 1, rotSpeed = 15;
    public float sizeScaleSpeed = 0.5f;
    public float scaleUpperBound = 1.25f;
    public float scaleLowerBound = 0.75f;

    void Update()
    {
        TranslateTransform();
        RotateTransform();
        ScaleTransform();
    }

    public void TranslateTransform()
    {
        //Vector3 pos = this.transform.position;

        //if(pos.x <= -1 || pos.x >= 1)
        //{
        //    movXSpeed *= -1;
        //}

        //if (pos.y <= -1 || pos.y >= 1)
        //{
        //    movYSpeed *= -1;
        //}

        //pos.x += Time.deltaTime * movXSpeed;
        //pos.y += Time.deltaTime * movYSpeed;

        //transform.position = pos;

        Mesh mesh = GetComponent<MeshFilter>().mesh;

        Vector3[] verts = mesh.vertices;

        foreach(Vector3 vert in verts)
        {

        }
    }

    // TODO: Rotation affects position, thus, gameobject will be needed to be centred upon creation
    public void RotateTransform()
    {
        transform.Rotate(0, 0, rotSpeed * Time.deltaTime);
    }

    // TODO: Fix the scale flicker
    public void ScaleTransform()
    {
        Vector3 sca = transform.localScale;

        if(sca.x <= scaleLowerBound || sca.x > scaleUpperBound)
        {
            sizeScaleSpeed *= -1;
        }

        sca.x += sizeScaleSpeed * Time.deltaTime;
        sca.y += sizeScaleSpeed * Time.deltaTime;

        transform.localScale = sca;
    }

    public Matrix3x3 Translate(float xOffset, float yOffset, Matrix3x3 matrix)
    {
        Matrix3x3 m = matrix;
        Vector3 v = matrix.GetColumn(2);

        v.Set(v.x + xOffset, v.y + yOffset, v.z);
        m.SetColumn(2, v);
        return m;
    }

    public Matrix3x3 Scale(float xScalar, float yScalar, Matrix3x3 matrix)
    {
        Matrix3x3 m = matrix;
        float[] arr = new float[3];

        //for(int i = 0; i < 3; i++)
        //{

        //}

        return new Matrix3x3();
    }

    public Matrix3x3 Rotate(float xOffset, float yOffset, Matrix3x3 matrix)
    {
        return new Matrix3x3();
    }

}
