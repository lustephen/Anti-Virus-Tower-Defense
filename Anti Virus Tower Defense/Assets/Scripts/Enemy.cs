using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int health = 100;
    public float speed = 5;
	void Start () {
		
	}
	// Update is called once per frame
	void Update () {
        gameObject.GetComponent<BoxCollider2D>().transform.position = gameObject.transform.position;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            print("HELLO");
            this.health -= collision.gameObject.GetComponent<BasicBullet>().damage;
            Destroy(collision.gameObject);
        }
    }
}
