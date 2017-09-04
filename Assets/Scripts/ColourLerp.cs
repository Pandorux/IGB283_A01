using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourLerp : MonoBehaviour {

    public Color32[] colours = new Color32[] {
        new Color32 (0, 0, 0, 0),
        new Color32 (255, 255, 255, 0)
    };

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Color32 ColourStep()
    {
        return new Color32 (0, 0, 0, 0);
    }
}
