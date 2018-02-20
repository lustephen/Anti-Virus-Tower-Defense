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


        GetInput();
                
	}

    private void GetInput() //Moving Camera Using WASD
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * cameraSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * cameraSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * cameraSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * cameraSpeed * Time.deltaTime);
        }

        //Sets Limit on Camera Movement
       // transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0, xMax), Mathf.Clamp(transform.position.y, yMin, 0), -10);

    }

    public void SetLimits(Vector3 maxTile)    //Prevent Camera From Moving Offscreen
    {
        Vector3 wp = Camera.main.ViewportToWorldPoint(new Vector3(1, 0));   //Location of Bottom Right Coordinate of Camera

        xMax = maxTile.x - wp.x;    //Max movement on x axis
        yMin = maxTile.y - wp.y;    //Min movement on y axis
    }
}
