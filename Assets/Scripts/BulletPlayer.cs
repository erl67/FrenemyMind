using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour {

    void Update () {
        gameObject.GetComponent<Transform>().Rotate(new Vector2(90f, 0f));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("enemy"))
        {
            //Debug.Log("BP Bullet hit " + other.tag);
            other.GetComponent<Collider2D>().enabled = false; //keeps you from overkill
            other.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            other.GetComponent<Rigidbody2D>().gravityScale = 1;
            other.GetComponent<Rigidbody2D>().mass = other.GetComponent<Rigidbody2D>().mass * .9f;
            Destroy(gameObject);
            ShipController.UpdateScore(1);
        }
    }

    void OnBecameInvisible()
    {
        //Debug.Log("BP Invisibile " + this.tag);
        Destroy(gameObject);
    }

}
