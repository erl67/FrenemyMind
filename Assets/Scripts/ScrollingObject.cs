using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour {

    public Rigidbody2D space2D; //or private?
    public float scrollSpeed;

    void Start () {
        space2D = GetComponent<Rigidbody2D>();
        scrollSpeed = -.5f;
        space2D.velocity = new Vector2(scrollSpeed, 0);
	}

    public void Stop ()
    {
        space2D.velocity = Vector2.zero;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
