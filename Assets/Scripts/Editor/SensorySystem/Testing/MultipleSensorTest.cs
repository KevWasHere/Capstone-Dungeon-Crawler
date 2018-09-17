using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleSensorTest : MonoBehaviour {
	private const float SPEED = 0.5f;
	private const float ANGLE = 10f;
	private const float CLOSE_ENOUGH_COSINE = 0.95f;

	private Transform target;
	private Renderer myRenderer;

	public Vector3[] wayPoint = {new Vector3 (4, 0, 0), new Vector3 (4, 0, 8),
		new Vector3 (-10, 0, 8), new Vector3 (-10, 0, 0), new Vector3(0, 0, 0),
		new Vector3 (0, 0, -6), new Vector3 (4, 0, -6)};

	public SNSMultiple sensor = null;
	private float u;

	/// <summary>
	/// Called to start this script.  It is critical when working with SNSSensor assets to call the
	/// sensor SetUp to make the current game object the origin
	/// 
	/// Date		Author	Description
	/// 2017-11-02	BRB		Initial Testing
	/// </summary>

	void Start () {
		target = GameObject.Find ("Player").transform;

		myRenderer = GetComponent<Renderer>();

		u = 0f;
	}

	/// <summary>
	/// Called by Unity every frame.  This method uses the sensor CanSee to change the color of the robot.
	/// It moves the robot linearly along the path defined by the waypoints
	/// 
	/// Date		Author	Description
	/// 2017-11-02	BRB		Initial Testing
	/// </summary>

	void Update () {
		myRenderer.material.color = sensor.CanSee (target)?Color.red:Color.green; // Use of sensor

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
