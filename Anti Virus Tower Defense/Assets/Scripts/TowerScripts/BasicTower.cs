using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BasicTower : MonoBehaviour {

    public float fireRadius = 2.0f;
    public float fireRate = 1.0f;
    public bool selected = false;
    public Object projectile;
    private Vector2 position;
    private GameObject target = null;
    private float fireTimer;
    private bool canFire;
	// Use this for initialization
	void Start () {
        gameObject.GetComponent<CircleCollider2D>().radius = fireRadius;
        fireTimer = 0.0f;
        canFire = true;
	}

    public void Init(Vector2 position)
    {
        this.position = position;
        gameObject.GetComponent<CircleCollider2D>().transform.position = position;
    }

    // Update is called once per frame
    void Update () {
        if (target != null)
        {
            if (canFire)
            {
                fire(target);
                canFire = false;
                fireTimer = 0.0f;
            }
        }
        if (selected)
        {
            drawRadiusCircle();
        }
        
        if (fireTimer >= fireRate)
        {
            canFire = true;
        }
        fireTimer += Time.deltaTime;
	}

    private void fire(GameObject target)
    {
        var bullet = PrefabUtility.InstantiatePrefab(projectile) as GameObject;
        var direction = target.transform.position - transform.position;
        direction.Normalize();
        bullet.GetComponent<BasicBullet>().Init(transform.position, direction);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy" && target == null)
        {
            target = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy") target = null;
    }

    private void drawRadiusCircle()
    {

    }
}
