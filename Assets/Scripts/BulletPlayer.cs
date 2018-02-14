using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour {

    void Update () {
        gameObject.GetComponent<Transform>().Rotate(new Vector2(90f, 0f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("enemy"))
        {
            Debug.Log("BP Bullet hit " + collision.tag);
            Destroy(collision.gameObject);
            Destroy(gameObject);
            ShipController.UpdateScore(1);
        }
    }

    void OnBecameInvisible()
    {
        Debug.Log("BP Invisibile " + this.tag);
        Destroy(gameObject);
    }

}
