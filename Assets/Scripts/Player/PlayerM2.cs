using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerM2 : MonoBehaviour
{
	// Start is called before the first frame update
	public int isReverse;
	public float dx = 0, dy = 0, dz = 0;
	public Animator ani;
	private bool findFloor;
	public int status;
	public int freeze;
	private bool isWaterScene =>
		SceneManager.GetActiveScene().name == "scene3";
	public bool dialogShown =>
		FindObjectOfType<TipsDialog>() != null;
	public bool talismanShown =>
		GameObject.FindGameObjectWithTag("Talisman") != null;
	public bool fall;
	public int startPosition;
	public bool collide;
	public Camera camera;
	int direction;
	bool cooldown;
	int recorder;
	Vector3 old_pos;
	bool isMoving;
	int timer;
	public float speed;
	public bool events;
	public Vector3 checkpoint;
	public int pushbackForce;
	void Start()
	{
		findFloor = false;
		status = 0;
		freeze = 1;
		startPosition = 0;
		fall = false;
		isMoving = false;
		old_pos = transform.position;
		timer = 0;
		GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
		collide = false;
		recorder = 0;
		cooldown = false;
		events = false;
		direction = 0;
		checkpoint = new Vector3(8.56f, 0.763f, -61.032f);

	}

	// Update is called once per frame
	void Update()
	{
		ani.SetFloat("status", status);
		ani.SetBool("isMoving", isMoving);
		if (Input.GetKeyUp("w") || Input.GetKeyUp("a") || Input.GetKeyUp("s") || Input.GetKeyUp("d"))
			{
				GetComponent<Rigidbody>().velocity = new Vector3(0 + dx, 0 + dy, 0 + dz);
			}
			if (Input.GetKey("w"))
			{
				direction = -1 * isReverse;
				status = -1 * isReverse;
				isMoving = true;
				if (Input.GetKey("a"))
				{
					GetComponent<Rigidbody>().velocity = new Vector3(-0.75f * speed * isReverse * freeze + dx, 0 + dy, 0.75f * speed * isReverse * freeze + dz);
					status = 2 * isReverse;
					direction = -3 * isReverse;
				}
				else if (Input.GetKey("d"))
				{
					GetComponent<Rigidbody>().velocity = new Vector3(0.75f * speed * isReverse * freeze + dx, 0, 0.75f * speed * isReverse * freeze + dz);
					status = -2 * isReverse;
					direction = 4 * isReverse;
				}
				else
				{
					GetComponent<Rigidbody>().velocity = new Vector3(0 + dx, 0, 1 * speed * isReverse * freeze + dz);
				}
			}
			else if (Input.GetKey("s"))
			{
				direction = 1 * isReverse;
				status = 1 * isReverse;
				isMoving = true;
				if (Input.GetKey("a"))
				{
					GetComponent<Rigidbody>().velocity = new Vector3(-0.75f * speed * isReverse * freeze + dx, 0, -0.75f * speed * isReverse * freeze + dz);
					status = 2 * isReverse;
					direction = -4 * isReverse;
				}
				else if (Input.GetKey("d"))
				{
					GetComponent<Rigidbody>().velocity = new Vector3(0.75f * speed * isReverse * freeze + dx, 0, -0.75f * speed * isReverse * freeze + dz);
					status = -2 * isReverse;
					direction = 3 * isReverse;
				}
				else
				{
					GetComponent<Rigidbody>().velocity = new Vector3(0 + dx, 0, -1 * speed * isReverse * freeze + dz);
				}
			}
			else if (Input.GetKey("a"))
			{
				direction = 2 * isReverse;
				status = 2 * isReverse;
				isMoving = true;
				if (Input.GetKey("w"))
				{
					GetComponent<Rigidbody>().velocity = new Vector3(-0.75f * speed * isReverse * freeze + dx, 0, 0.75f * speed * isReverse * freeze + dz);
					direction = -3 * isReverse;
				}
				else if (Input.GetKey("s"))
				{
					GetComponent<Rigidbody>().velocity = new Vector3(-0.75f * speed * isReverse * freeze + dx, 0, -0.75f * speed * isReverse * freeze + dz);
					direction = -4 * isReverse;
				}
				else
				{
					GetComponent<Rigidbody>().velocity = new Vector3(-1 * speed * isReverse * freeze + dx, 0, 0 + dz);
				}
			}
			else if (Input.GetKey("d"))
			{
				//GetComponent<Rigidbody>().velocity = new Vector3(3 * isReverse * freeze, 0, 0);
				direction = -2 * isReverse;
				status = -2 * isReverse;
				isMoving = true;
				if (Input.GetKey("w"))
				{
					GetComponent<Rigidbody>().velocity = new Vector3(0.75f * speed * isReverse * freeze + dx, 0, 0.75f * speed * isReverse * freeze + dz);
					direction = 4 * isReverse;
				}
				else if (Input.GetKey("s"))
				{
					GetComponent<Rigidbody>().velocity = new Vector3(0.75f * speed * isReverse * freeze + dx, 0, -0.75f * speed * isReverse * freeze + dz);
					direction = 3 * isReverse;
				}
				else
				{
					GetComponent<Rigidbody>().velocity = new Vector3(1 * speed * isReverse * freeze + dx, 0, 0);
				}
			}
			else
			{
				//status = 0;
				isMoving = false;
			}
	}
}
