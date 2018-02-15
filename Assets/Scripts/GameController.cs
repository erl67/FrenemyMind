using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public static GameController instance;

    public GameObject enemyCraftPrefab;
    public Transform enemyBulletSpawn;
    public GameObject enemyBulletPrefab;

    public GameObject asteroidPrefab;

    public AudioSource enemyBulletFire;
    public AudioSource background;

    private float speed;

    private float timeElapsed, startTime, interval;
    private float volume;

    private float speedXMin, speedXMax, speedYMin, speedYMax, speedX, speedY, gMin, gMax, massMin, massMax, scaleMin, scaleMax;
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
        AudioListener.volume = .2f;
        background = GetComponent<AudioSource>();   //background music

        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                speedXMin = 20f;
                speedXMax = 100f;
                speedYMin = -5f;
                speedYMax = 5f;
                gMin = -.01f;
                gMax = .01f;
                massMin = .9f;
                massMax = 1.1f;
                scaleMin = .9f;
                scaleMax = 1.1f;
                interval = .1f;
                break;
            case 1:
                speedXMin = 50f;
                speedXMax = 200f;
                speedYMin = -10f;
                speedYMax = 10f;
                gMin = -.1f;
                gMax = .1f;
                massMin = .7f;
                massMax = 1.3f;
                scaleMin = .5f;
                scaleMax = 1.5f;
                interval = .2f;
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

        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayerDead();
            //UnityEditor.EditorApplication.isPlaying = false;  //hide for build
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Pause))
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

        if (Input.GetKeyDown(KeyCode.F1))
        {
            ShipController.health = 999;
            ShipController.goal = 99;
            ShipController.weaponsLoad = 999;
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.Home))
        {
            SceneManager.LoadScene(0);
        }

        timeElapsed += Time.deltaTime;

        if (Time.frameCount % 100 == 0 && spawn)
        {
            Vector3 spawnOffset = new Vector3(0f, Random.Range(6f, -4f), 0f);
            var enemy = (GameObject)Instantiate(enemyCraftPrefab, transform.position + spawnOffset, transform.rotation);

            scale = enemy.GetComponent<Rigidbody2D>().transform.localScale * Random.Range(scaleMin, scaleMax);
            enemy.GetComponent<Rigidbody2D>().transform.localScale = scale;

            speedX = Random.Range(speedXMin, speedXMax) * -1;
            speedY = Random.Range(speedYMin, speedYMax);
            enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(speedX, speedY));

            enemy.GetComponent<Rigidbody2D>().gravityScale = Random.Range(gMin, gMax);
            enemy.GetComponent<Rigidbody2D>().mass = Random.Range(massMin, massMax);
        }

        if (timeElapsed % 3 < interval && spawn && rocks)
        {
            //Vector3 spawnOffset = new Vector3(Random.Range(9f, 12f), Random.Range(-5F, 5f), 0f);  //testing different spawn locations
            //Vector3 spawnOffset = new Vector3(0f, Random.Range(-5F, 5f), 0f);
            Vector3 spawnOffset = new Vector3(Random.Range(0f, 2f), Random.Range(-7f, 7f), 0f);

            var rock = (GameObject)Instantiate(asteroidPrefab, transform.position + spawnOffset, transform.rotation);

            rock.GetComponent<Transform>().Rotate(new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(0f, 90f)));  //make rocks look different

            scale = rock.GetComponent<Rigidbody2D>().transform.localScale * Random.Range(scaleMin, scaleMax);
            rock.GetComponent<Rigidbody2D>().transform.localScale = scale;
            rock.GetComponent<Rigidbody2D>().mass = rock.GetComponent<Rigidbody2D>().transform.localScale.x * rock.GetComponent<Rigidbody2D>().transform.localScale.y;

            speedX = Random.Range(-20f, -250f);
            speedY = Random.Range(0f, 20f);
            rock.GetComponent<Rigidbody2D>().AddForce(new Vector2(speedX, speedY));

            rock.GetComponent<Rigidbody2D>().gravityScale = Random.Range(gMin, gMax);
            rock.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-60f, 60f);
        }

    }

    void OnBecameInvisible()
    {
        Debug.Log("GC Invisibile" + this.tag);
    }

    public void MuteBG()
    {
        background.mute = true;     //doesn't work on it's own in PlayerDead()
    }

    public void PlayerDead()
    {
        MuteBG();
        spawn = false;
        gameOver = true;
    }

}
