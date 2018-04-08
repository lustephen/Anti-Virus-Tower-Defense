using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTower : MonoBehaviour {

    public float fireRadius = 2.0f;
    private Vector2 position;
    private Transform target = null;
	// Use this for initialization
	void Start () {
        gameObject.GetComponent<CircleCollider2D>().radius = fireRadius;
	}

    public void Init(Vector2 position)
    {
        this.position = position;
        gameObject.GetComponent<CircleCollider2D>().transform.position = position;
    }

    // Update is called once per frame
    void Update () {
        if (target == null) return;
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            target = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy") target = null;
    }
}
