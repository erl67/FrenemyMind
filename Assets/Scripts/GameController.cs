using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject enemyCraftPrefab;
    private float speed;

	// Use this for initialization
	void Start () {
        var time = Time.time;

        var enemy = (GameObject)Instantiate(enemyCraftPrefab, transform.position, transform.rotation);

        var speed = Random.Range(10f, 50f) * -1;
        //enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(speed, 0f));

        enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(-10f, 0f));

    }

    // Update is called once per frame
    void Update () {
        //var enemy = (GameObject)Instantiate(enemyCraftPrefab, transform.position, transform.rotation);
		
	}

    private void OnTriggerEnter2D(Collider other)
    {
        Debug.Log("Enemey collision " + other.tag);
        if (other.tag.Equals("boundary"))
        {
            Destroy(gameObject);
        }
    }
}
