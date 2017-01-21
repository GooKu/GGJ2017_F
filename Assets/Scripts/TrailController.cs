using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailController : MonoBehaviour {
    enum Type
    {
        dayTime = 0,
        Twilight = 1,
        night = 2
    }

    public Material[] materials;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SetMaterial(Type dayStatus)
    {
        GetComponent<TrailRenderer>().sharedMaterial = materials[(int)dayStatus];
    }
}
