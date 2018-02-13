using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour {

    void Update()
    {
        gameObject.GetComponent<Transform>().Rotate(new Vector2(90f, 0f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("player"))
        {
            Debug.Log("BE Bullet hit " + collision.tag);
            Destroy(collision.gameObject);
            Destroy(gameObject);
            ShipController.UpdateHealth(-1);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
