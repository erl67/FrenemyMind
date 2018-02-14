using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Asteroid : MonoBehaviour {

    public Transform asteroidSpawn;
    public GameObject asteroidPrefab;

    public AudioSource rockSound;

    private float timeElapsed, interval, speed, speedMin, speedMax, gMin, gMax, mMin, mMax;

    void Start()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                interval = 0.01f;
                break;
            case 1:
                speedMin = 10f;
                speedMax = 200f;
                gMin = -.2f;
                gMax = .2f;
                mMin = .7f;
                mMax = 1.3f;
                interval = 0.1f;
                break;
            default:
                break;
        }
        timeElapsed = 0;
    }

    void Update()
    {

        if (GetComponent<Renderer>().isVisible == false)    //remove things that aren't on screen
        {
            Destroy(gameObject);
        }

        timeElapsed += Time.deltaTime;

        if (timeElapsed % 3 < interval)
        {
            gameObject.GetComponent<Transform>().Rotate(new Vector2(90f, 0f));

            //    //Transform spawn = this.transform;
            //    //Transform spawn = new Transform(10f, 0f, 0f);
            //    Vector3 spawnOffset = new Vector3(0f, Random.Range(-5f, 5f), 0f);

            //    GameObject rock = (GameObject)Instantiate(asteroidPrefab, transform.position + spawnOffset, transform.rotation);
            //    rock.GetComponent<Rigidbody2D>().AddForce(new Vector2(10f, 0f) * -1 * Random.Range(speedMin, speedMax));
            //    speed = Random.Range(speedMin, speedMax) * -1;
            //    rock.GetComponent<Rigidbody2D>().AddForce(new Vector2(speed, 0f));
            //    rock.GetComponent<Rigidbody2D>().gravityScale = Random.Range(gMin, gMax);
            //    rock.GetComponent<Rigidbody2D>().mass = Random.Range(mMin, mMax);
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
            Destroy(gameObject);
        }

        if (other.tag.Equals("asteroid"))
        {
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Debug.Log("Asteroid Invisibile");
        Destroy(gameObject);
    }
}
