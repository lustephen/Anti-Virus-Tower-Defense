using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour {

    public float speed = 2.0f;
    public int damage = 25;
    private Vector3 direction;
    // Use this for initialization
    void Start() {

    }

    public void Init(Vector2 position, Vector3 direction)
    {
        transform.position = new Vector3(position.x, position.y, 0);
        this.direction = direction;
    }
	
	// Update is called once per frame
	void Update () {
        print(direction);
        transform.position += direction * speed * Time.deltaTime;
   
	}
}
