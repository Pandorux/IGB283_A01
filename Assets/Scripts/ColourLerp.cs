using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ColourLerp : MonoBehaviour {

    public Color32 colour00 = new Color32(0, 0, 0, 0), colour01 = new Color32(255, 255, 255, 255);


	void Start () {
        this.GetComponent<MeshRenderer>().material.shader = Shader.Find("Unlit/Color");
    }
	

	void Update () {
        Vector3 meshOrigin = this.GetComponent<MeshRenderer>().bounds.center;
        UpdateColour(meshOrigin);
	}

    public Color32 ColourStep(Vector3 meshCentre)
    {
        return Color32.Lerp(colour00, colour01, Mathf.Clamp(meshCentre.x, -0.5f, 0.5f) * 2);
    }

    void UpdateColour(Vector3 meshCentre)
    {       
        this.GetComponent<MeshRenderer>().material.color = ColourStep(meshCentre);
    }
}
