using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour {

    private Rigidbody2D space2D;
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
	
	void Update () {
        if (GameController.instance.gameOver)
        {
            space2D.velocity = Vector2.zero;
        }
    }
}
