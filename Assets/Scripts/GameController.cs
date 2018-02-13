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
        }

        if (!spawn && Input.GetKeyDown(KeyCode.N))
        {
            Debug.Log(SceneManager.GetActiveScene().ToString());
        }

        if (Time.frameCount % 100 == 0 && spawn)
        {
            speed = Random.Range(20f, 100f) * -1;
            Vector3 spawnOffset = new Vector3(0f, Random.Range(0f, -10f), 0f);
            var enemy = (GameObject)Instantiate(enemyCraftPrefab, transform.position + spawnOffset, transform.rotation);
            enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(speed, 0f));
            enemy.GetComponent<Rigidbody2D>().gravityScale = Random.Range(-.05f, .05f);
            enemy.GetComponent<Rigidbody2D>().mass = Random.Range(.7f, 1.1f);

            //GameObject eb = (GameObject)Instantiate(enemyBulletPrefab, transform.position, transform.rotation);
            //Vector2 motion = new Vector2(10f, 0f);
            //eb.GetComponent<Rigidbody2D>().AddForce(motion * -20);

            //Debug.Log(enemy.GetComponent<Rigidbody2D>().gravityScale);
        }

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
        gameOver = true;
    }



}
