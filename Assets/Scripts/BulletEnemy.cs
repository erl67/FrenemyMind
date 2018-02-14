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
        gameObject.GetComponent<Transform>().Rotate(new Vector2(Random.Range(80f, 90f), 0f));
        //if (gameObject.GetComponent<Renderer>().isVisible == false)
        //{
        //    Destroy(gameObject);
        //}
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("BE Bullet hit " + other.tag);

        if (other.tag.Equals("player")) //handled in player class
        {
        }
        if (other.tag.Equals("space") || other.tag. Equals("asteroid"))
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
