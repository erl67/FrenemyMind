using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyShip1 : MonoBehaviour {

    public Transform enemyBulletSpawn;
    public GameObject enemyBulletPrefab;

    public AudioSource fire;    //using audio as a component of the prefab rather than a source

    private float timeElapsed;
    private float interval, speedMin, speedMax, duration;

    void Start () {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                speedMin = 5f;
                speedMax = 30f;
                duration = 3f;
                interval = 0.05f;
                break;
            case 1:
                speedMin = 30f;
                speedMax = 100f;
                duration = 6f;
                interval = 0.1f;
                break;
            default:
                break;
        }
        timeElapsed = 0;
        fire = gameObject.GetComponent<AudioSource>();
    }
	
	void Update () {

        if (GetComponent<Renderer>().isVisible == false)    //remove things that aren't on screen
        {
            Destroy(gameObject);
        }

        timeElapsed += Time.deltaTime;

        if (timeElapsed % 3 < interval && GameController.instance.spawn == true)
        {
            Transform spawn = this.transform;
            GameObject eb = (GameObject)Instantiate(enemyBulletPrefab, spawn.position, spawn.rotation);
            eb.GetComponent<Rigidbody2D>().AddForce(new Vector2(10f, 0f) * -1 * Random.Range(speedMin, speedMax));
            Destroy(eb, duration);
            //Debug.Log("Enemy Firing " + timeElapsed + " " + timeElapsed % 3);
            fire.Play();
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("EnemyShip1 collision " + other.tag);

        if (other.tag.Equals("enemy"))
        {
            Destroy(other.gameObject);
        }

        if (other.tag.Equals("enemybullet"))
        {
            //Destroy(other.gameObject);
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
        //Debug.Log("EsC Invisibile");
        Destroy(gameObject);
    }
}
