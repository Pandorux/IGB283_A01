using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Moveable : MonoBehaviour
{

    public GameObject thingo;
    public GameObject partner;
    private bool update;

    void Start()
    {
        update = false;
        this.transform.position = new Vector3(this.transform.position.x, thingo.GetComponent<MeshFilter>().mesh.bounds.center.y, this.transform.position.z);
    }

    void Update()
    {     
        OnMouseClick();
        OnMouseDrag();
        OnMouseRelease();
    }

    void OnMouseClick()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && this.GetComponent<MeshFilter>().mesh.bounds.IntersectRay(Camera.main.ScreenPointToRay(Input.mousePosition)))
        {
            update = true;
            Debug.Log("Working");
        }
    }

    void OnMouseRelease()
    {
        if(Input.GetKeyUp(KeyCode.Mouse0)) {
            update = false;
            Debug.Log("Not Working");
        }
    }

    void OnMouseDrag()
    {
        if(update)
        {
            float mouseY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
            Debug.Log(mouseY);
            this.transform.position = new Vector3(transform.position.x, Mathf.Clamp(mouseY, -1, 1), transform.position.z);
            partner.transform.position = new Vector3(partner.transform.position.x, Mathf.Clamp(mouseY, -1, 1), partner.transform.position.z);
            Debug.Log("Dragging");
            //thingo.GetComponent<IGB283Transform>().ySpeed = Input.mousePosition.y - thingo.GetComponent<MeshFilter>().mesh.bounds.center.y;
            //this.transform.position = new Vector3(this.transform.position.x, thingo.GetComponent<MeshFilter>().mesh.bounds.center.y, this.transform.position.z);

        }
    }
}
