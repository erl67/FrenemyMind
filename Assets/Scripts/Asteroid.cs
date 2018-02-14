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
    private float timeElapsed, interval, spinAmount;
    private int spinRate;

    void Start()
    {
        timeElapsed = 0;
        spinRate = Random.Range(5, 15);
        spinAmount = Random.Range(10f, 90f);
        interval = .3f; //minimum asteroid mass
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
            GetComponent<Rigidbody2D>().transform.localScale = GetComponent<Rigidbody2D>().transform.localScale * Random.Range(.9f, .99f);
            GetComponent<Rigidbody2D>().mass = 1f * GetComponent<Rigidbody2D>().transform.localScale.x * GetComponent<Rigidbody2D>().transform.localScale.y;
        }

        if (other.tag.Equals("playerbullet"))
        {
            Destroy(other.gameObject);
            GetComponent<Rigidbody2D>().transform.localScale = GetComponent<Rigidbody2D>().transform.localScale * Random.Range(.4f, .8f);
            GetComponent<Rigidbody2D>().mass = 1f * GetComponent<Rigidbody2D>().transform.localScale.x * GetComponent<Rigidbody2D>().transform.localScale.y;

            if (GetComponent<Rigidbody2D>().mass > interval)
            {
                damaged.Play();
            } else
            {
                GetComponent<Collider2D>().enabled = false; //keeps you from overkill
                GetComponent<Rigidbody2D>().mass = 0;
                GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                GetComponent<Rigidbody2D>().angularVelocity = Random.Range(7f, 20f);

                destroyed.Play();
                Destroy(gameObject, 2f); //delay destroy so sound plays
                ShipController.UpdateScore(1);
            }
        }
    }

    void OnBecameInvisible()
    {
        //Debug.Log("Asteroid Invisibile");
        Destroy(gameObject);
    }
}
