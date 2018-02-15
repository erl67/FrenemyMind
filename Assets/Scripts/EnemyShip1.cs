using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyShip1 : MonoBehaviour {

    public Transform enemyBulletSpawn;
    public GameObject enemyBulletPrefab;

    public AudioSource[] audioSources;
    public AudioSource fire, oof;    //using audio as a component of the prefab rather than a source

    private float timeElapsed;
    private float interval, speedMin, speedMax, duration;
    private float x, y, m;

    private bool masterArmSwitch = false;

    void Start () {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                speedMin = 5f;
                speedMax = 30f;
                duration = 4f;
                interval = 0.05f;
                break;
            case 1:
                speedMin = 30f;
                speedMax = 100f;
                duration = 8f;
                interval = 0.1f;
                break;
            default:
                break;
        }
        timeElapsed = 0;
        AudioSource[] audioSources = GetComponents<AudioSource>();
        fire = audioSources[0];
        oof = audioSources[1];
        masterArmSwitch = true;
    }
	
	void Update () {

        timeElapsed += Time.deltaTime;

        if (timeElapsed % 3 < interval && GameController.instance.spawn == true && masterArmSwitch == true) //enemy shooting
        {
            Transform spawn = this.transform;
            GameObject eb = (GameObject)Instantiate(enemyBulletPrefab, spawn.position, spawn.rotation);
            eb.GetComponent<Rigidbody2D>().AddForce(new Vector2(-10f, 0f) * Random.Range(speedMin, speedMax));
            Destroy(eb, duration);
            fire.Play();
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("EnemyShip1 collision " + other.tag);
        if (other.tag.Equals("playerbullet"))
        {
            oof.Play();
            ShipDies();
        }

        if (other.tag.Equals("enemy"))  
        {
            //was testing a bounce effect, never worked right
            //x = GetComponent<Rigidbody2D>().velocity.x;
            //y = GetComponent<Rigidbody2D>().velocity.y * 1.1f;
            //m = GetComponent<Rigidbody2D>().velocity.magnitude;
            //GetComponent<Rigidbody2D>().velocity = new Vector2(x, y) * m;
            GetComponent<Rigidbody2D>().gravityScale = GetComponent<Rigidbody2D>().gravityScale * 1.1f;
        }

        if (other.tag.Equals("spaceship"))
        {
            ShipDies();
        }

        if (other.tag.Equals("asteroid") && other.GetComponent<Rigidbody2D>().mass > 1.3f)
        {
            ShipDies();
        }
    }

    private void ShipDies()
    {
        masterArmSwitch = false;

        GetComponent<Collider2D>().enabled = false; //keeps you from overkill
        GetComponent<SpriteRenderer>().color = new Color(0.850f, 0.788f, 0.788f, .9f);

        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-30f, -180f);

        GetComponent<Rigidbody2D>().gravityScale = 1;
        GetComponent<Rigidbody2D>().mass = GetComponent<Rigidbody2D>().mass * .5f;
    }

    void OnBecameInvisible()
    {
        //Debug.Log("EsC Invisibile");
        Destroy(gameObject);
    }
}
