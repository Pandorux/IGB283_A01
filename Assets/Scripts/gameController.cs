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

            // Add scripts to gameobjects
            gameObjArray[i].AddComponent<GraphicalObject>();
        }
    }
}
