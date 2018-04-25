using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestID : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    if(Application.isEditor)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
	}
}
