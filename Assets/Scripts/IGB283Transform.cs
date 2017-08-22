using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IGB283Transform {

    public static int x = 0;
    public static int y = 1;
    public static int z = 2;


    public static void TranslateTransform()
    {
        
    }

    public static void RotateTransform(float angle, GameObject obj)
    {
        Vector3[] origVerts = obj.GetComponent<MeshFilter>().mesh.vertices;
        Vector3[] newVerts = new Vector3[origVerts.Length];
        rot = Quaternion.Euler(obj.transform.rotation.x, obj.transform.rotation.y, obj.transform.rotation.z);
        Transform mat = obj.transform.Rotate(obj.transform.rotation.x, obj.transform.rotation.y, obj.transform.rotation.z);
        Matrix4x4.Rotate




    }

    public static void ScaleTransform()
    {

    }
}
