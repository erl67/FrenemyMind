using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBackground : MonoBehaviour {

    private float hLength = 19f;

	void Update () {
        if (transform.position.x < -hLength)
        {
            RepositionBackground();
        }
    }

    void RepositionBackground()
    {
        Vector2 offset = new Vector2(hLength * 2f, 0f);
        transform.position = (Vector2) transform.position + offset;
    }
    
}
