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
    }

    void OnBecameInvisible()
    {
        //Debug.Log("BP Invisibile " + this.tag);
        Destroy(gameObject);
    }

}
