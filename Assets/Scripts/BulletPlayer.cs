using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.GetComponent<Transform>().Rotate(new Vector2(90f, 0f));
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("enemy")) {
            Debug.Log("Bullet hit " + collision.tag);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

    }



}
