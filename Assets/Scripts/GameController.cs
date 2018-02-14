using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public static GameController instance;
    public static ShipController sc;

    public GameObject enemyCraftPrefab;
    public Transform enemyBulletSpawn;
    public GameObject enemyBulletPrefab;

    public AudioSource enemyBulletFire;
    public AudioSource background;

    private float speed;

    private float timeElapsed;
    private float startTime;

    public bool gameOver;
    public bool spawn = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        gameOver = false;
    }

    void Start () {
    }

    void Update () {

        if (GameController.instance.gameOver) {
            PlayerDead();
        }

        if (gameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //SceneManager.LoadScene(0);
        }

        if (!spawn && Input.GetKeyDown(KeyCode.N))
        {
            Destroy(gameObject);
            spawn = true;
            //Debug.Log(SceneManager.GetActiveScene().ToString());
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Time.frameCount % 100 == 0 && spawn)
        {
            speed = Random.Range(50f, 100f) * -1;
            //Vector3 stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            //Debug.Log(stageDimensions.x + " " + stageDimensions.y);
            Vector3 spawnOffset = new Vector3(0f, Random.Range(4f, -4f), 0f);
            var enemy = (GameObject)Instantiate(enemyCraftPrefab, transform.position + spawnOffset, transform.rotation);
            enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(speed, 0f));
            enemy.GetComponent<Rigidbody2D>().gravityScale = Random.Range(-.03f, .03f);
            enemy.GetComponent<Rigidbody2D>().mass = Random.Range(.95f, 1.05f);
        }

    }

    void OnBecameInvisible()
    {
        Debug.Log("GC Invisibile");
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("GC Enemy collision " + other.tag);
        if (other.tag.Equals("boundary"))
        {
            PlayerDead();
            Destroy(gameObject);
        }
    }

    public void PlayerDead()
    {
        background = gameObject.GetComponent<AudioSource>();
        background.mute = true;
        spawn = false;
        gameOver = true;
    }

}
