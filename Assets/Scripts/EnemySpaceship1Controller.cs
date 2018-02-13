using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpaceship1Controller : MonoBehaviour {

    public Transform enemyBulletSpawn;
    public GameObject enemyBulletPrefab;

    public AudioSource enemyBulletFire;

    void Start () {
        //rb = gameObject.GetComponent<Rigidbody2D>();
    }
	
	void Update () {
        if (Time.frameCount % 200 == 0)
        {
            //enemyBulletSpawn = GameObject.Find("enemy").transform;
            GameObject eb = (GameObject)Instantiate(enemyBulletPrefab, enemyBulletSpawn.position, enemyBulletSpawn.rotation);
            Vector2 motion = new Vector2(10f, 0f);
            eb.GetComponent<Rigidbody2D>().AddForce(motion * -30);

            //Vector3 spawnOffset = new Vector3(0f, Random.Range(0f, -10f), 0f);
            //var eb = (GameObject)Instantiate(enemyBulletPrefab, transform.position + spawnOffset, transform.rotation);
            //eb.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(20f, 100f), 0f));
            //eb.GetComponent<Rigidbody2D>().gravityScale = Random.Range(-.05f, .05f);
            //eb.GetComponent<Rigidbody2D>().mass = Random.Range(.7f, 1.1f);

            //Debug.Log(enemy.GetComponent<Rigidbody2D>().gravityScale);
            //gameObject.GetComponent<AudioSource>().Play();
            //bulletFire.Play();

            Debug.Log("Enemy Firing\n");

            Destroy(eb, 4f);
        }


    }

}
