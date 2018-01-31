using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public static GameController instance;

    public GameObject enemyCraftPrefab;
    private float speed;

    private float timeElapsed;
    private float startTime;

    //private bool newEnemy;

    public bool gameOver;

    private void Awake()
    {
        gameOver = false;
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start () {
        timeElapsed = 0;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update () {

        if (GameController.instance.gameOver) {
            //space2D.velocity = Vector2.zero;

            //ScrollingObject.space2D.velocity = Vector2.zero;
            //var background = new ScrollingObject();
            //background.Stop();
        }

        if (gameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        timeElapsed += Time.deltaTime;

        //timeElapsed = Time.time;
        Debug.Log(message: "start " + timeElapsed + " mod " + ((int) timeElapsed % 3));
        

        if (Time.frameCount % 100 == 0)
        {
            Vector3 spawnOffset = new Vector3(0f, Random.Range(-5f, 5f), 0f);

            var enemy = (GameObject) Instantiate(enemyCraftPrefab, spawnOffset, transform.rotation);

            speed = Random.Range(1f, 30f) * -1;
            enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(speed, 0f));

        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Enemey collision " + other.tag);
        if (other.tag.Equals("boundary"))
        {
            Destroy(gameObject);
        }
    }

    public void PlayerDead()
    {
        gameOver = true;
    }
}
