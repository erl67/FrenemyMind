using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip1 : MonoBehaviour {

    public Transform enemyBulletSpawn;
    public GameObject enemyBulletPrefab;

    public AudioSource enemyBulletFire;

    
    void Start () {
		
	}
	
	void Update () {

        if (Time.frameCount % 400 == 0)
        {
            //this is the only way i could get the enemy prefabs to shoot
            var enemies = GameObject.FindGameObjectsWithTag("enemy");
            Debug.Log(enemies.Length);

            //for (int i=0; i<enemies.Length; i+=2)
            //{
                //if (Random.Range(0,1) == 1)
                //{
                    //Transform spawn = enemies[i].transform;
                    Transform spawn = this.transform;
                    GameObject eb = (GameObject)Instantiate(enemyBulletPrefab, spawn.position, spawn.rotation);
                    Vector2 motion = new Vector2(10f, 0f);
                    eb.GetComponent<Rigidbody2D>().AddForce(motion * -20);
                    //Debug.Log(enemies[i].name);
                    Destroy(eb, 3f);
                //}
            //}



            //gameObject.GetComponent<AudioSource>().Play();
            //bulletFire.Play();

            Debug.Log("Enemy Firing\n");

            //Destroy(eb, 4f);
        }

    }
}
