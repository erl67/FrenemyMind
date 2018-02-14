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

    public GameObject asteroidPrefab;

    public AudioSource enemyBulletFire;
    public AudioSource background;

    private float speed;

    private float timeElapsed;
    private float startTime;

    private float speedMin, speedMax, gMin, gMax, mMin, mMax;

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
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                speedMin = 20f;
                speedMax = 70f;
                gMin = -.03f;
                gMax = .03f;
                mMin = .9f;
                mMax = 1.1f;
                break;
            case 1:
                speedMin = 50f;
                speedMax = 100f;
                gMin = -.2f;
                gMax = .2f;
                mMin = .7f;
                mMax = 1.3f;
                break;
            default:
                break;
        }
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
            PlayerDead();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }

        if (!spawn && Input.GetKeyDown(KeyCode.Q))
        {
            PlayerDead();
            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
        }

        if (Time.frameCount % 100 == 0 && spawn)
        {
            //Vector3 stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            //Debug.Log(stageDimensions.x + " " + stageDimensions.y);
            Vector3 spawnOffset = new Vector3(0f, Random.Range(6f, -4f), 0f);
            var enemy = (GameObject)Instantiate(enemyCraftPrefab, transform.position + spawnOffset, transform.rotation);
            speed = Random.Range(speedMin, speedMax) * -1;
            enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(speed, 0f));
            enemy.GetComponent<Rigidbody2D>().gravityScale = Random.Range(gMin, gMax);
            enemy.GetComponent<Rigidbody2D>().mass = Random.Range(mMin, mMax);
        }

        if (Time.frameCount % 120 == 0 && spawn)
        {
            Vector3 spawnOffset = new Vector3(Random.Range(10f, -10f), Random.Range(10f, -10f), 0f);
            var rock = (GameObject)Instantiate(asteroidPrefab, transform.position + spawnOffset, transform.rotation);
            speed = Random.Range(speedMin, speedMax + 300f) * -1;
            rock.GetComponent<Rigidbody2D>().AddForce(new Vector2(speed, 0f));
            rock.GetComponent<Rigidbody2D>().gravityScale = Random.Range(gMin - .5f, gMax + .5f);
            rock.GetComponent<Rigidbody2D>().mass = Random.Range(mMin - .5f, mMax + 1f);
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
