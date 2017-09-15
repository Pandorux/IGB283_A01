using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ColourLerp : MonoBehaviour {

    public Color32 colour00 = new Color32(0, 0, 0, 0), colour01 = new Color32(255, 255, 255, 255);

	void Start () {
        // Sets the shader to unlit to make the colour change easier to see
        this.GetComponent<MeshRenderer>().material.shader = Shader.Find("Unlit/Color");
    }

	void Update () {
        Vector3 meshOrigin = this.GetComponent<MeshRenderer>().bounds.center;
        UpdateColour(meshOrigin);
	}

    // Uses the mesh location to step between the two colours
    public Color32 ColourStep(Vector3 meshCentre)
    {
        float step = Mathf.Clamp((meshCentre.x + 1) / 2, 0, 1);
        return Color32.Lerp(colour00, colour01, step);
    }

    void UpdateColour(Vector3 meshCentre)
    {       
        this.GetComponent<MeshRenderer>().material.color = ColourStep(meshCentre);
    }
}
