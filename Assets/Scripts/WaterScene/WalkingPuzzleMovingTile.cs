using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingPuzzleMovingTile : MonoBehaviour
{
	// Start is called before the first frame update
	float speedX,speedZ,speedXFixed,speedZFixed; //speedX and Z used for velocity calculation, speedXFixed and Z used for set a fixed speed quantity.
	public float expectedTimeUse; // use time to calculation the expected moving pattern
	public float xDisplacement;
	public float zDisplacement;
	float X1, X2, Z1, Z2;
	void Start()
	{
		speedXFixed = xDisplacement/expectedTimeUse;
		speedZFixed = zDisplacement/expectedTimeUse;
		speedX = speedXFixed;
		speedZ = speedZFixed;
		X1 = GetComponent<Rigidbody>().transform.position.x;
		X2 = X1 + xDisplacement;
		Z1 = GetComponent<Rigidbody>().transform.position.z;
		Z2 = Z1 + zDisplacement;
	}

	// Update is called once per frame
	void Update()
	{
		if (xDisplacement > 0 && zDisplacement > 0)
		{
			GetComponent<Rigidbody>().velocity = new Vector3(0.3f * speedX, 0, 0.3f * speedZ);
			if (GetComponent<Rigidbody>().transform.position.x > X2 || GetComponent<Rigidbody>().transform.position.z > Z2)
			{
				speedX = -speedXFixed;
				speedZ = -speedZFixed;
			}
			if (GetComponent<Rigidbody>().transform.position.x < X1 || GetComponent<Rigidbody>().transform.position.z < Z1)
			{
				speedX = speedXFixed;
				speedZ = speedZFixed;
			}
		}
		if (xDisplacement > 0 && zDisplacement < 0)
		{
			GetComponent<Rigidbody>().velocity = new Vector3(0.3f * speedX, 0, 0.3f * speedZ);
			if (GetComponent<Rigidbody>().transform.position.x > X2 || GetComponent<Rigidbody>().transform.position.z < Z2)
			{
				speedX = -speedXFixed;
				speedZ = -speedZFixed;
			}
			if (GetComponent<Rigidbody>().transform.position.x < X1 || GetComponent<Rigidbody>().transform.position.z > Z1)
			{
				speedX = speedXFixed;
				speedZ = speedZFixed;
			}
		}
		if (xDisplacement < 0 && zDisplacement > 0)
		{
			GetComponent<Rigidbody>().velocity = new Vector3(0.3f * speedX, 0, 0.3f * speedZ);
			if (GetComponent<Rigidbody>().transform.position.x < X2 || GetComponent<Rigidbody>().transform.position.z > Z2)
			{
				speedX = -speedXFixed;
				speedZ = -speedZFixed;
			}
			if (GetComponent<Rigidbody>().transform.position.x > X1 || GetComponent<Rigidbody>().transform.position.z < Z1)
			{
				speedX = speedXFixed;
				speedZ = speedZFixed;
			}
		}
		if (xDisplacement < 0 && zDisplacement < 0)
		{
			GetComponent<Rigidbody>().velocity = new Vector3(0.3f * speedX, 0, 0.3f * speedZ);
			if (GetComponent<Rigidbody>().transform.position.x < X2 || GetComponent<Rigidbody>().transform.position.z < Z2)
			{
				speedX = -speedXFixed;
				speedZ = -speedZFixed;
			}
			if (GetComponent<Rigidbody>().transform.position.x > X1 || GetComponent<Rigidbody>().transform.position.z > Z1)
			{
				speedX = speedXFixed;
				speedZ = speedZFixed;
			}
		}
	}
}
