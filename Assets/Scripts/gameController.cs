using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int numberOfGameObjects = 1;
    public int minVerts = 5;
    public int maxVerts = 8;

    public float posRange = 1;
    public float minSize = 0.025f;
    public float maxSize = 0.1f;

    public float minMovSpeed = 0.5f;
    public float maxMovSpeed = 2.5f;

    public float minRotSpeed = 2.5f;
    public float maxRotSpeed = 25;

    // Use this for initialization
    void Awake()
    {
        // Container
        GameObject[] gameObjArray = new GameObject[numberOfGameObjects];

        // Add the components
        for (int i = 0; i < gameObjArray.Length; i++)
        {
            gameObjArray[i] = new GameObject();
            gameObjArray[i].AddComponent<GraphicalObject>();

            GraphicalObject gra = new GraphicalObject(); 
            gra.xSize = Random.Range(minVerts, maxVerts);
            gra.ySize = Random.Range(minVerts, maxVerts);

            IGB283Transform trans = gameObjArray[i].GetComponent<IGB283Transform>();
            trans.initialPosition = new Vector3(Random.Range(-posRange, posRange), Random.Range(-posRange, posRange), 0);
            //trans.initialScale = new Vector3(Random.Range(-minSize, maxSize), Random.Range(-minSize, maxSize));
            trans.xSpeed = Random.Range(minMovSpeed, maxMovSpeed);
            trans.ySpeed = Random.Range(minMovSpeed, maxMovSpeed);
            trans.rotSpeed = Random.Range(minRotSpeed, maxRotSpeed);

            ColourLerp col = gameObjArray[i].GetComponent<ColourLerp>();
            col.colour00 = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255));
            col.colour01 = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255));
           
        }
    }
}
