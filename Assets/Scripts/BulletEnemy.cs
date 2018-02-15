using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour {

    //public static BulletEnemy instance;

    void Start()
    {
    }

    void Update()
    {
        gameObject.GetComponent<Transform>().Rotate(new Vector2(Random.Range(45f, 90f), 0f));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("BE Bullet hit " + other.tag);

        if (other.tag.Equals("space") || other.tag. Equals("asteroid") || other.tag.Equals("spaceship"))
        {
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        //Debug.Log("BE Invisibile");
        Destroy(gameObject);
    }
}
