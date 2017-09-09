using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IGB283Transform {

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
