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
        //TranslateTransform();
        //RotateTransform();
        //ScaleTransform();
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

    public static Matrix3x3 Translate(Vector3 offset)
    {
        Matrix3x3 m = new Matrix3x3();

        m.SetRow(0, new Vector3(1.0f, 0.0f, offset.x));
        m.SetRow(1, new Vector3(0.0f, 1.0f, offset.y));
        m.SetRow(2, new Vector3(0.0f, 0.0f, 1.0f));

        return m;
    }

    public static Matrix3x3 Scale(Quaternion scale)
    {
        Matrix3x3 m = new Matrix3x3();

        m.SetRow(0, new Vector3(scale.x, 0.0f, 0.0f));
        m.SetRow(1, new Vector3(0.0f, scale.y, 0.0f));
        m.SetRow(2, new Vector3(0.0f, 0.0f, 1.0f));

        return m;
    }

    public static Matrix3x3 Rotate(float angle)
    {

        Matrix3x3 m = new Matrix3x3();

        m.SetRow(0, new Vector3(Mathf.Cos(angle), -Mathf.Sin(angle), 0.0f));
        m.SetRow(1, new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0.0f));
        m.SetRow(2, new Vector3(0.0f, 0.0f, 1.0f));

        return m;
    }
}
