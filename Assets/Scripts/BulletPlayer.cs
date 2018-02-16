using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("enemy"))
        {
            //Debug.Log("BP Bullet hit " + other.tag);
            Destroy(gameObject);
            ShipController.UpdateScore(1);
        }

        if (other.tag.Equals("asteroid"))
        {
            Destroy(gameObject);
        }

        if (other.tag.Equals("enemybullet"))
        {
            if (Random.Range(0, 10) > 5)
            {
                GetComponent<Collider2D>().enabled = false;
                GetComponent<Rigidbody2D>().angularVelocity = Random.Range(0f, 1000f);
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, Random.Range(-200f, 200f)));
                Destroy(gameObject, 3f);
            }
        }
    }

    void OnBecameInvisible()
    {
        //Debug.Log("BP Invisibile " + this.tag);
        Destroy(gameObject);
    }

}
