using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShipController : MonoBehaviour {

    public float speed = 10f;

    public int shotCounter = 0;
    public int weaponsLoad = 50;
    public static int score = 0;
    public static int health = 50;
    public static int goal = 10;

    private Rigidbody2D rb;

    public Text txtScore;
    public Text txtAmmo;
    public Text txtLives;
    public Text txtLevel;
    public Text txtLevelEnd;

    public Transform bulletSpawn;
    public GameObject bulletPrefab;

    public AudioSource bulletFire;
    public AudioSource bc;
    public AudioSource rockSound;
    public AudioSource enemyCollision;
    public AudioSource over;

    private void Awake()
    {
        txtLevelEnd.text = " ";
    }

    void Start () {
        rb = gameObject.GetComponent <Rigidbody2D>();
        GameController.instance.spawn = true;

        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                score = 0;
                health = 10;
                goal = 5;
                break;
            case 1:
                //score = score;
                health = 100;
                goal = 20;
                break;
            default:
                break;
        }

        txtScore.text = "Score: " + score;
        txtAmmo.text = "Ammo: " + (weaponsLoad - shotCounter);
        txtLives.text = "Health: " + health;
        Time.timeScale = 1;
        Object.Destroy(txtLevel, 3f);
    }

    void FixedUpdate() {

        float moveH = Input.GetAxis("Horizontal");
        float moveV = Input.GetAxis("Vertical");
        float mouseH = Input.GetAxis("Mouse X");
        float mouseV = Input.GetAxis("Mouse Y");

        if (moveH != 0f || moveV != 0f)
        {
            Vector2 motion = new Vector2(moveH, moveV);
            rb.AddForce(motion);
            rb.AddForce(motion * speed);
            rb.mass = rb.mass * 0.9999f;
            //Debug.Log(rb.transform.position.x + " " + rb.transform.position.y);
        }
        else if (mouseH != 0f || mouseV != 0f)
        {
            Vector2 motion = new Vector2(mouseH * 2f, mouseV * 2f);
            rb.AddForce(motion);
            rb.AddForce(motion * speed);
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Fire();
        }

        if (Input.GetMouseButtonDown(1))
        {
            GameController.instance.PlayerDead();
        }

        //Debug.Log("Motion:" + motion + " Mass:" + rb.mass + " Speed:" + speed);
        txtScore.text = "Score: " + score;
        txtLives.text = "Health: " + health;

        if (score >= goal)
        {
            GameController.instance.spawn = false;
            txtLevelEnd.text = "The End of this Level\nYour Final Score is: " + score.ToString();
            if (SceneManager.GetActiveScene().buildIndex != 1) txtLevelEnd.text += "\nPress (n) for next level";
            txtLevelEnd.text += "\nPress (q) to quit";
            Time.timeScale = 0;
            //StartCoroutine(EndLevel(50f));
        }

        if (health <= 0)
        {
            bulletFire.mute = true;
            bc.mute = true;
            over.Play();
            GameController.instance.spawn = false;
            if (txtLevel != null) txtLevel.text = "";
            txtLevelEnd.text = "Game Over\nYour Final Score is: " + score.ToString();
            txtLevelEnd.text += "\nPress (r) to continue";
            Time.timeScale = 0;
            GameController.instance.PlayerDead();
            //StartCoroutine(EndLevel(5f));
        }

        if (gameObject.GetComponent<Renderer>().isVisible == false)
        {
            //GameController.instance.PlayerDead();
        }

    }

    void Fire()
    {
        if (shotCounter < weaponsLoad)
        {
            bulletFire.Play();

            GameObject bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

            Vector2 motion = new Vector2(10f, 0f);
            bullet.GetComponent<Rigidbody2D>().AddForce(motion * speed * (3 + SceneManager.GetActiveScene().buildIndex));

            shotCounter++;
            txtAmmo.text = "Ammo: " + (weaponsLoad - shotCounter);

            Debug.Log("Firing\n");
            Destroy(bullet, 6f);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("ShipController collision " + other.tag);

        if (other.tag.Equals("enemy"))
        {
            health--;
            enemyCollision.Play();
            Destroy(other.gameObject);
        }

        if (other.tag.Equals("enemybullet"))
        {
            //bc = other.GetComponent<AudioSource>();
            bc.Play();
            Destroy(other.gameObject);
            health--;
        }

        if (other.tag.Equals("space")) //stop player from fleeing
        {
            Vector2 current = rb.velocity;
            rb.velocity = Vector3.zero;
            rb.AddForce(current * -3f);
        }

        if (other.tag.Equals("asteroid"))
        {
            rockSound.Play();
            health = health - (int) (other.GetComponent<Rigidbody2D>().mass * 10f);
            Destroy(other.gameObject);
        }
    }

    public static void UpdateScore(int amount)
    {
        score += amount;
    }

    public static void UpdateHealth(int amount)
    {
        health += amount;
    }

    void OnBecameInvisible()
    {
        Debug.Log("SC Invisibile " + this.tag);
        health = 0;
        GameController.instance.PlayerDead();
    }

    IEnumerator EndLevel(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        //Time.timeScale = 1;
    }

}