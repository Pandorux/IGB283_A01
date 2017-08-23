using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IGB283Transform : MonoBehaviour {

    public float movYSpeed = 5, movXSpeed = 5, rotSpeed = 5;
    public float sizeScaleSpeed;

    void Start()
    {

    }

    void Update()
    {
        TranslateTransform();
        RotateTransform();
        ScaleTransform();
    }

    public void TranslateTransform()
    {
        Vector3 pos = this.transform.position;
        Mesh mesh = gameObject.GetComponent<MeshFilter>().mesh;

        if(mesh.bounds.extents.x >= 1 || -mesh.bounds.extents.x <= -1)
        {
            movXSpeed *= -1;
        }

        if (mesh.bounds.extents.y >= 1 || -mesh.bounds.extents.y <= -1)
        {
            movYSpeed *= -1;
        }

        pos.x += Time.deltaTime * movXSpeed;
        pos.y += Time.deltaTime * movYSpeed;

        transform.position = pos;
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

        if(sca.x > 0.5 || sca.x <= 0.2)
        {
            sizeScaleSpeed *= -1;
        }

        sca.x += sizeScaleSpeed * Time.deltaTime;
        sca.y += sizeScaleSpeed * Time.deltaTime;

        transform.localScale = sca;
    }
}
