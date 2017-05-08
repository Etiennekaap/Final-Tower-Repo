using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public static int resources;
    public int startResources = 800;

    public static int integrity;
    public int startIntegrity = 100;

	void Start ()
    {
        resources = startResources;
        integrity = startIntegrity;
	}
	
}
