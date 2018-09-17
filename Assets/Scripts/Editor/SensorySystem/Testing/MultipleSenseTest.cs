using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleSenseTest: MonoBehaviour {

	public SNSMultiple sensor;

	private Transform target;
	private Renderer myRenderer;

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
	}
	
	// Update is called once per frame
	void Update () {
		myRenderer.material.color = sensor.CanSee (target)?Color.red:Color.green; // Use of sensor
	}
}
