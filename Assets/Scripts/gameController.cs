using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public int numberOfGameObjects = 1;

    // Use this for initialization
    void Start()
    {
        // Container
        GameObject[] gameObjArray = new GameObject[numberOfGameObjects];

        // Add the components
        for (int i = 0; i < gameObjArray.Length; i++)
        {
            gameObjArray[i] = new GameObject();

            // Add all the scripts
            gameObjArray[i].AddComponent<GraphicalObject>();
            gameObjArray[i].AddComponent<IGB283Transform>();

            // Set the size scale, rotational speed and movement speed
            IGB283Transform tran = gameObjArray[i].GetComponent<IGB283Transform>();

            tran.rotSpeed = (i + 1) * 20;
            tran.sizeScaleSpeed = (float)(i + 1) / 4;
            tran.movXSpeed = (i + 1) * 1.25f;
            tran.movYSpeed = 0;
        }
    }
}
