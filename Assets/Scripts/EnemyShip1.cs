using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip1 : MonoBehaviour {

    public Transform enemyBulletSpawn;
    public GameObject enemyBulletPrefab;

    public AudioSource fire;    //using audio as a component of the prefab rather than a source

    private float timeElapsed;

    void Start () {
        timeElapsed = 0;
        fire = gameObject.GetComponent<AudioSource>();
    }
	
	void Update () {

        if (GetComponent<Renderer>().isVisible == false)    //remove things that aren't on screen
        {
            Destroy(gameObject);
        }

        timeElapsed += Time.deltaTime;

        //if (timeElapsed % 3 < Random.Range(0f, .3f))
        if (timeElapsed % 3 < 0.07 && GameController.instance.spawn == true)
        {
            Transform spawn = this.transform;
            GameObject eb = (GameObject)Instantiate(enemyBulletPrefab, spawn.position, spawn.rotation);
            Vector2 motion = new Vector2(10f, 0f);
            eb.GetComponent<Rigidbody2D>().AddForce(motion * -1 * Random.Range(20f, 30f));
            Destroy(eb, 3f);
            Debug.Log("Enemy Firing " + timeElapsed + " " + timeElapsed % 3);
            fire.Play();
        }

    }

    void OnBecameInvisible()
    {
        Debug.Log("EsC Invisibile");
        Destroy(gameObject);
    }
}
