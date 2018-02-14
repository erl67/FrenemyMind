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
    private float volume;

    private float speedMin, speedMax, gMin, gMax, mMin, mMax, sMin, sMax, interval;
    private Vector3 scale;

    public bool gameOver;
    public bool spawn = true;
    private bool rocks = false;

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
        AudioListener.volume = AudioListener.volume * .2f;

        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                speedMin = 20f;
                speedMax = 70f;
                gMin = -.03f;
                gMax = .03f;
                mMin = .9f;
                mMax = 1.1f;
                sMin = .9f;
                sMax = 1.1f;
                interval = .1f;
                break;
            case 1:
                speedMin = 50f;
                speedMax = 100f;
                gMin = -.2f;
                gMax = .2f;
                mMin = .7f;
                mMax = 1.3f;
                sMin = .5f;
                sMax = 1.5f;
                interval = .1f;
                rocks = true;
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

        if (Input.GetKeyDown(KeyCode.P))
        {
            volume = Time.timeScale == 1 ? AudioListener.volume : volume;
            Time.timeScale = Time.timeScale == 1 ? 0 : 1;
            spawn = spawn == true ? false : true;
            AudioListener.volume = Time.timeScale == 0 ? 0f : volume; 
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            AudioListener.volume = AudioListener.volume * .9f;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            AudioListener.volume = AudioListener.volume * 1.05f;
        }

        timeElapsed += Time.deltaTime;

        if (Time.frameCount % 100 == 0 && spawn)
        {
            //Vector3 stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            //Debug.Log(stageDimensions.x + " " + stageDimensions.y);
            Vector3 spawnOffset = new Vector3(0f, Random.Range(6f, -4f), 0f);
            var enemy = (GameObject)Instantiate(enemyCraftPrefab, transform.position + spawnOffset, transform.rotation);
            scale = enemy.GetComponent<Rigidbody2D>().transform.localScale * Random.Range(sMin, sMax);
            enemy.GetComponent<Rigidbody2D>().transform.localScale = scale;
            speed = Random.Range(speedMin, speedMax) * -1;
            enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(speed, 0f));
            enemy.GetComponent<Rigidbody2D>().gravityScale = Random.Range(gMin, gMax);
            enemy.GetComponent<Rigidbody2D>().mass = Random.Range(mMin, mMax);
        }

        //if (Time.frameCount % 120 == 0 && spawn && rocks)
        if (timeElapsed % 3 < interval && spawn && rocks)
        {
            Vector3 spawnOffset = new Vector3(Random.Range(10f, -10f), Random.Range(10f, -10f), 0f);
            var rock = (GameObject)Instantiate(asteroidPrefab, transform.position + spawnOffset, transform.rotation);
            rock.GetComponent<Transform>().Rotate(new Vector2(Random.Range(0f, 45f), 0f));
            scale = rock.GetComponent<Rigidbody2D>().transform.localScale * Random.Range(sMin, sMax);
            rock.GetComponent<Rigidbody2D>().transform.localScale = scale;
            speed = Random.Range(speedMin - 20f, speedMax + 20f) * -1;
            rock.GetComponent<Rigidbody2D>().AddForce(new Vector2(speed, 0f));
            rock.GetComponent<Rigidbody2D>().gravityScale = Random.Range(gMin, gMax + .5f);
            rock.GetComponent<Rigidbody2D>().mass = 1f * rock.GetComponent<Rigidbody2D>().transform.localScale.x * rock.GetComponent<Rigidbody2D>().transform.localScale.y;
        }

    }

    void OnBecameInvisible()
    {
        Debug.Log("GC Invisibile" + this.tag);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("GC Enemy collision " + other.tag);
        if (other.tag.Equals("boundary"))
        {
            PlayerDead();
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
