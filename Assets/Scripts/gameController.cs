using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour
{

    public int numberOfGameObjects = 1;

    // Use this for initialization
    void Start()
    {
        // Create the game object
        GameObject obj1 = new GameObject();
        GameObject obj2 = new GameObject();

        // Container
        GameObject[] gameObjArray = new GameObject[numberOfGameObjects];

        // Add the components
        for (int i = 0; i < gameObjArray.Length; i++)
        {
            gameObjArray[i] = new GameObject();

            // Add all the scripts
            gameObjArray[i].AddComponent<graphicalObject>();
            gameObjArray[i].AddComponent<IGB283Transform>();

            // Set the size scale and rotational speed
            IGB283Transform tran = gameObjArray[i].GetComponent<IGB283Transform>();

            tran.rotSpeed = (i + 1) * 20;
            tran.sizeScaleSpeed = (float)(i + 1) / 4;

            // Se graphical size
            graphicalObject go = gameObjArray[i].GetComponent<graphicalObject>();

            go.xSize = 5;
            go.ySize = 5;

            // Finally move them sideways
            Vector3 pos = gameObjArray[i].transform.position;
            pos.x = -5 + 10 * i;
            gameObjArray[i].transform.position = pos;
        }

    }
}
