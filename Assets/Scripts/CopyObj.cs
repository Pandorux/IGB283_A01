using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyObj : MonoBehaviour
{

    /* Public Variables */
    public GameObject prefab;

    // Use this for initialization
    void Start()
    {
        // Create the game object
        GameObject obj1 = new GameObject();
        GameObject obj2 = new GameObject();

        // Container
        List<GameObject> list = new List<GameObject>();

        // Add to container
        list.Add(obj1);
        list.Add(obj2);

        // Add the components
        for (int i = 0; i < list.Count; i++)
        {
            // Add all the scripts
            list[i].AddComponent<MeshRenderer>();
            list[i].AddComponent<MeshFilter>();
            list[i].AddComponent<graphicalObject>();
            list[i].AddComponent<IGB283Transform>();

            // Set the size scale and rotational speed
            IGB283Transform tran = list[i].GetComponent<IGB283Transform>();

            tran.rotSpeed = (i + 1) * 20;
            tran.sizeScaleSpeed = (float)(i + 1) / 4;

            // Se graphical size
            graphicalObject go = list[i].GetComponent<graphicalObject>();

            go.xSize = 5;
            go.ySize = 5;

            // Finally move them sideways
            Vector3 pos = list[i].transform.position;
            pos.x = -5 + 10 * i;
            list[i].transform.position = pos;
        }

    }
}
