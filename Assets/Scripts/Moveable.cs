﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Moveable : MonoBehaviour
{

    public GameObject thingo;
    public GameObject partner;
    public int count;
    private bool update;

    void Start()
    {
        update = false;
        this.transform.position = new Vector3(this.transform.position.x, thingo.GetComponent<MeshFilter>().mesh.bounds.center.y, this.transform.position.z);
        this.GetComponent<BoxCollider>().size *= 2;
    }

    void Update()
    {     
        OnMouseRelease();
        OnMouseDrag();
        OnMouseClick();
    }

    void OnMouseClick()
    {
        RaycastHit hit;
        Ray rayray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(rayray, out hit, 1000);
        if (Input.GetKeyDown(KeyCode.Mouse0) && hit.transform.gameObject == this.gameObject)
        {
            update = true;
        }
    }

    void OnMouseRelease()
    {
        if(Input.GetKeyUp(KeyCode.Mouse0)) {
            update = false;
        }
    }

    void OnMouseDrag()
    {
        if(update)
        {
            float mouseY = Mathf.Clamp(Camera.main.ScreenToWorldPoint(Input.mousePosition).y, -1, 1);
            this.transform.position = new Vector3(transform.position.x, mouseY, transform.position.z);
            partner.transform.position = new Vector3(partner.transform.position.x, mouseY, partner.transform.position.z);
            thingo.GetComponent<IGB283Transform>().ySpeed = mouseY - thingo.GetComponent<MeshFilter>().mesh.bounds.center.y;
        }
    }
}
