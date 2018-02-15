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
    private float timeElapsed, minSize, spinAmount;
    private int spinRate;

    void Start()
    {
        timeElapsed = 0;
        spinRate = Random.Range(5, 20);
        spinAmount = Random.Range(10f, 90f);
        minSize = .3f; //minimum asteroid mass
        AudioSource[] audioSources = GetComponents<AudioSource>();
        damaged = audioSources[0];
        destroyed = audioSources[1];
    }

    void Update()
    {
        timeElapsed = timeElapsed + Time.deltaTime;

        if (Time.frameCount % spinRate == 0)
        {
            gameObject.GetComponent<Transform>().Rotate(new Vector3(0f, 0f, spinAmount));
        }

        if (GetComponent<Renderer>().isVisible == false)    //remove things that aren't on screen
        {
            Debug.Log("invisible asteroid");
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("spaceship"))
        {
            ReduceAsteroid(.4f, .6f);
            AsteroidDies();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag.Equals("asteroid"))
        {
            ReduceAsteroid(.995f, 1f);
        }
        if (other.tag.Equals("spaceship"))
        {
            ReduceAsteroid(.98f, 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Asteroid collision " + other.tag);

        if (other.tag.Equals("enemy"))
        {
            ReduceAsteroid(.8f, .9f);
        }

        if (other.tag.Equals("asteroid"))
        {
            ReduceAsteroid(.9f, .98f);
        }

        if (other.tag.Equals("playerbullet"))
        {
            ReduceAsteroid(.5f, .9f);

            if (GetComponent<Rigidbody2D>().mass > minSize)
            {
                damaged.Play();
            } else
            {
                destroyed.Play();
                AsteroidDies();
                ShipController.UpdateScore(1);
            }
        }
    }

    private void ReduceAsteroid(float min, float max)    //collision reduces up to 10% of asteroid
    {
        GetComponent<Rigidbody2D>().transform.localScale = GetComponent<Rigidbody2D>().transform.localScale * Random.Range(min, max);
        GetComponent<Rigidbody2D>().mass = GetComponent<Rigidbody2D>().transform.localScale.x * GetComponent<Rigidbody2D>().transform.localScale.y;
    }

    private void AsteroidDies()
    {
        GetComponent<Collider2D>().enabled = false; //keeps you from overkill
        GetComponent<Rigidbody2D>().mass = 0;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().angularVelocity = GetComponent<Rigidbody2D>().angularVelocity * Random.Range(-.5f, .5f);
        GetComponent<SpriteRenderer>().color = new Color(1f, .9f, .9f, .5f);

        Destroy(gameObject, 2f); //delay destroy so sound plays
    }

    void OnBecameInvisible()
    {
        //Debug.Log("Asteroid Invisibile");
        Destroy(gameObject);
    }
}
