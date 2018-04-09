using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FlyingCraftController : MonoBehaviour {

	private float rotationAngleDegree;
	private float rotationSpeedDegree;
	int rotationDirection;

	private Vector3 motionVelocity;
	private float motionSpeedXZ;
	private int motionDirectionXZ;

	private float motionSpeedY;
	private int motionDirectionY;

	public GameController gameController;

	// Use this for initialization
	void Start ()
	{
		rotationAngleDegree = 0;
		rotationSpeedDegree = 100;
		rotationDirection = 0;
		motionVelocity = Vector3.zero;
		motionSpeedXZ = 5;
		motionDirectionXZ = 0;
		motionSpeedY = 2.0f;
		motionDirectionY = 0;
	}

	// Update is called once per frame
	void Update ()
	{

		motionDirectionY = 0;

		if (true)
		{
    	motionDirectionXZ = 1;
    	move();
		}

		if (Input.GetKey("right"))
		{
			rotationDirection = 1;
			Rotate();
		}

		if (Input.GetKey("left"))
		{
			rotationDirection = -1;
			Rotate();
		}

		if (Input.GetKey("up"))
		{
    	motionDirectionY = 1;
			move();
		}

		if (Input.GetKey("down"))
		{
    	motionDirectionY = -1;
			move();
		}

	}

	private void Rotate ()
	{
		float rotationVelocityDegree = rotationSpeedDegree * rotationDirection;
		rotationAngleDegree += rotationVelocityDegree * Time.deltaTime;

		//make sure that rotationAngleDegree within o-360
		rotationAngleDegree = (rotationAngleDegree + 360) % 360;
		transform.Rotate(Vector3.up, rotationVelocityDegree * Time.deltaTime); //vector.up is Y-Axis
	}

	private void move()
	{
    //convert degree to radian
    double rotationAngleRadian = ((float)rotationAngleDegree / 360.0) * (Math.PI * 2.0);
    float motionX = (float)Math.Sin(rotationAngleRadian) * motionDirectionXZ;
		float motionZ = (float)Math.Cos(rotationAngleRadian) * motionDirectionXZ;
		//Add the following
		float motionY = motionDirectionY;

    motionVelocity = new Vector3(motionX, motionY, motionZ);

    //nomoralized vector to represent the directions of motionVelocity
    motionVelocity.Normalize();

		motionVelocity = new Vector3(motionVelocity.x * motionSpeedXZ * -1, motionVelocity.y * motionSpeedY, motionVelocity.z * motionSpeedXZ * -1);
    transform.position += motionVelocity * Time.deltaTime;

    rotationDirection = 0;
    motionDirectionXZ = 0;
	}

	void OnTriggerEnter(Collider c)
	{
    //you will update these in a later step
    if (c.gameObject.tag == "Obstacle") {
        gameController.GameOverLose();
    } else if (c.gameObject.tag == "Ground") {
        gameController.GameOverLose();
    }
    /*if (c.gameObject.tag == "Gap") {
        gameController.addScore();
				c.gameObject.SetActive (false);
    }*/
	}

	void OnTriggerExit(Collider c)
	{
    if (c.gameObject.tag == "Gap")
		{
      gameController.addScore();
			c.gameObject.SetActive (false);
    }
	}
}
