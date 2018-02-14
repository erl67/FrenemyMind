using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Asteroid : MonoBehaviour {

    public Transform asteroidSpawn;
    public GameObject asteroidPrefab;

    public AudioSource[] audioSources;
    public AudioSource damaged;    //using audio as a component of the prefab rather than a source
    public AudioSource destroyed;
    private float timeElapsed, interval;

    void Start()
    {
        timeElapsed = 0;
        interval = .2f;
        AudioSource[] audioSources = GetComponents<AudioSource>();
        damaged = audioSources[0];
        destroyed = audioSources[1];
    }

    void Update()
    {
        if (GetComponent<Renderer>().isVisible == false)    //remove things that aren't on screen
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Asteroid collision " + other.tag);

        if (other.tag.Equals("enemy"))
        {
            Destroy(other.gameObject);
        }

        if (other.tag.Equals("space"))
        {
            //Destroy(gameObject);
        }

        if (other.tag.Equals("asteroid"))
        {
            //Destroy(gameObject);
            //Destroy(other.gameObject);
        }

        if (other.tag.Equals("playerbullet"))
        {
            //rockSound = GetComponents<AudioSource>()[0];
            //rockSound.Play();

            Destroy(other.gameObject);
            GetComponent<Rigidbody2D>().transform.localScale = GetComponent<Rigidbody2D>().transform.localScale * Random.Range(.5f, .8f);
            GetComponent<Rigidbody2D>().mass = 1f * GetComponent<Rigidbody2D>().transform.localScale.x * GetComponent<Rigidbody2D>().transform.localScale.y;

            if (GetComponent<Rigidbody2D>().mass > .4f)
            {
                damaged.Play();
            } else
            {
                GetComponent<Rigidbody2D>().mass = 0;
                destroyed.Play();
                Destroy(gameObject, 1f); //delay destroy so sound plays
            }
        }
    }

    void OnBecameInvisible()
    {
        //Debug.Log("Asteroid Invisibile");
        //Destroy(gameObject);
    }
}
