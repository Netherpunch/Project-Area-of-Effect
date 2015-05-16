using UnityEngine;
using System.Collections;

public class Camera_Follow : MonoBehaviour
{

	public Transform target; //Camera Will follow this target

	//Distance from target
	public float height = 10;

	public float angle = 0;
	public float rotationalSpeed = 100;

	public float radius = 10;

	// Use this for initialization
	void Start ()
	{
		target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update ()
	{
		float CameraX = target.position.x + (radius * Mathf.Cos(angle));
		float CameraY = target.position.y + height;
		float CameraZ = target.position.z + (radius * Mathf.Sin(angle));

		transform.position = new Vector3 (CameraX, CameraY, CameraZ);

		if (Input.GetKey (KeyCode.A))
		{
			angle = angle - rotationalSpeed * Time.deltaTime;
		}
		else if (Input.GetKey (KeyCode.D))
		{
			angle = angle + rotationalSpeed * Time.deltaTime;
		}

		transform.LookAt (target.position);
	}
}
