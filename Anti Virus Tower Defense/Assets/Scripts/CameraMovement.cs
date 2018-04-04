using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    [SerializeField]    //Can acces from inspector 
    private float cameraSpeed = 0;

    private float xMax;
    private float yMin;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	private void Update () {


		if (GetInput ()) {
			//Sets Limit on Camera Movement (needs fixing)
			/* transform.position = new Vector3 (Mathf.Clamp (transform.position.x, 0, xMax),
				Mathf.Clamp (transform.position.y, yMin, 0), -10); */
		}

    }

    private bool GetInput() //Moving Camera Using WASD
    {
		bool input = false;
		if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * cameraSpeed * Time.deltaTime);
			input = true;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * cameraSpeed * Time.deltaTime);
			input = true;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * cameraSpeed * Time.deltaTime);
			input = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * cameraSpeed * Time.deltaTime);
			input = true;
        }
		return input;

        //Sets Limit on Camera Movement
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0, xMax), Mathf.Clamp(transform.position.y, yMin, 0), -10);


    }

    public void SetLimits(Vector3 maxTile)    //Prevent Camera From Moving Offscreen
    {
        Vector3 wp = Camera.main.ViewportToWorldPoint(new Vector3(1, 0));   //Location of Bottom Right Coordinate of Camera
      //  Vector3 wp = new Vector3(0, 0, 0);
        Debug.Log(maxTile.x);
        Debug.Log(wp.x);
        //xMax = maxTile.x - wp.x;    //Max movement on x axis
        //yMin = maxTile.y - wp.y;    //Min movement on y axis
        xMax = wp.x - maxTile.x;    //Max movement on x axis
        yMin = maxTile.y - wp.y;    //Min movement on y axis
    }
}
