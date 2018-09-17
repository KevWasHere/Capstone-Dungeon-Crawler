using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	private const float SPEED = 0.5f;
	private const float ANGLE = 10f;
	private const float CLOSE_ENOUGH_COSINE = 0.95f;

	public Vector3[] wayPoint = {new Vector3 (4, 0, 0), new Vector3 (4, 0, 8),
		new Vector3 (-10, 0, 8), new Vector3 (-10, 0, 0), new Vector3(0, 0, 0),
		new Vector3 (0, 0, -6), new Vector3 (4, 0, -6)};

	private float u = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		int back = Mathf.FloorToInt (u);
		int ahead = (back + 1) % wayPoint.Length;

		Vector3 delta = wayPoint[ahead] - transform.position;
		delta.Normalize();

		Quaternion facing = Quaternion.LookRotation (delta);

		if (Vector3.Dot (delta, transform.forward) > CLOSE_ENOUGH_COSINE) {
			transform.rotation = facing;
			transform.position = Vector3.Lerp (wayPoint[back], wayPoint[ahead], u - back);
			u += SPEED * Time.deltaTime;
			if (u >= wayPoint.Length)
				u -= wayPoint.Length;
		} else {
			transform.rotation = Quaternion.RotateTowards (transform.rotation, facing, ANGLE + Time.deltaTime);
		}
	}
}
