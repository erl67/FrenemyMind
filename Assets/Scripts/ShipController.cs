using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipController : MonoBehaviour {

    public float speed = 10f;
    public float mass = 10f;

    private int shotCounter = 0;
    private int weaponsLoad = 75;
    public Text txtAmmo;
    public Text txtScore;

    public Transform bulletSpawn;
    public GameObject bulletPrefab;

    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent <Rigidbody2D>();
        rb.mass = mass;
        rb.gravityScale = 0;
        txtAmmo.text = "Ammo: " + (weaponsLoad - shotCounter);
	}

    // Update is called once per frame
    void FixedUpdate() {

        float moveH = Input.GetAxis("Horizontal");
        float moveV = Input.GetAxis("Vertical");

        //Debug.Log("H=" + moveH + " V=" + moveV + "\n");

        Vector2 motion = new Vector2(moveH, moveV);

        if (moveH != 0f || moveV != 0f)
        {
            rb.AddForce(motion);
            rb.AddForce(motion * speed);
            rb.mass = rb.mass * 0.999f;

            Debug.Log("moving\n");
        }



        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }


        Debug.Log("Motion:" + motion + " Mass:" + rb.mass + " Speed:" + speed);

    }

    void Fire()
    {
        if (shotCounter <= weaponsLoad)
        {
            GameObject bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            //var bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation) as GameObject;

            Vector2 motion = new Vector2(10f, 0f);

            bullet.GetComponent<Rigidbody2D>().AddForce(motion * speed * 3);

            shotCounter++;
            txtAmmo.text = "Ammo: " + (weaponsLoad - shotCounter);

            Debug.Log("Firing\n");
            Destroy(bullet, 4f);
        }



    }

    void OnCollisionEnter2D()
    {
        Debug.Log("Something Collided");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name + " " + other.tag);
    }




}