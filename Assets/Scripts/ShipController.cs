using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipController : MonoBehaviour {

    public float speed = 10f;

    public int shotCounter = 0;
    public int weaponsLoad = 50;
    public static int score = 0;
    public static int health = 2;
    public static int l1goal = 2;
    public static int l2goal = 10;

    private Rigidbody2D rb;

    public Text txtScore;
    public Text txtAmmo;
    public Text txtLives;
    public Text txtLevel;
    public Text txtLevelEnd;

    public Transform bulletSpawn;
    public GameObject bulletPrefab;

    public AudioSource bulletFire;

	void Start () {
        rb = gameObject.GetComponent <Rigidbody2D>();
        //rb.gravityScale = 0;
        
        UpdateScore(0);
        txtAmmo.text = "Ammo: " + (weaponsLoad - shotCounter);
        txtLives.text = "Health: " + health;

        Object.Destroy(txtLevel, 2.0f);
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

        //txtLevelEnd.text = endMessage;

        if (score >= l1goal)
        {
            GameController.instance.spawn = false;
            txtLevelEnd.text = "The End of this Level\nYour Final Score is: " + score.ToString();
            txtLevelEnd.text += "\nPress (n) for next level";
            Time.timeScale = 0;
            StartCoroutine(EndLevel(5f));
            score = 0;
        }

        if (health <= 0)
        {
            Destroy(gameObject);
            GameController.instance.PlayerDead();
            txtLevelEnd.text = "Game Over\nYour Final Score is: " + score.ToString();
            txtLevelEnd.text += "\nPress (r) to continue";
            Time.timeScale = 0;
            StartCoroutine(EndLevel(5f));
            score = 0;
        }

    }

    void Fire()
    {
        if (shotCounter < weaponsLoad)
        {
            //gameObject.GetComponent<AudioSource>().Play();
            bulletFire.Play();

            GameObject bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

            Vector2 motion = new Vector2(10f, 0f);
            bullet.GetComponent<Rigidbody2D>().AddForce(motion * speed * 3);

            shotCounter++;
            txtAmmo.text = "Ammo: " + (weaponsLoad - shotCounter);

            Debug.Log("Firing\n");
            Destroy(bullet, 4f);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("ShipController collision " + other.name + " " + other.tag);

        if (other.tag.Equals("enemy"))
        {
            health--;
            Destroy(other.gameObject);
        }

        if (other.tag.Equals("enemybullet"))
        {
            health--;
            Destroy(other.gameObject);
        }

        if (other.tag.Equals("space"))
        {
            Vector2 current = rb.velocity;
            rb.velocity = Vector3.zero;
            rb.AddForce(current * -3f);
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
        if (this.tag.Equals("spaceship"))
        {
            this.rb.transform.position = new Vector3(-5f, -4f, 0);
            health = 0;
        } else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator EndLevel(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        Time.timeScale = 1;
    }

}