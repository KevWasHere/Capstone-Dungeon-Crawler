using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Base class that all sensors are derived from
/// 
/// Constant			Description
/// EPSILON				Floating point comparisson fuzz value
/// NAME_WIDTH			Width of name field in inspector displays
/// 
/// Field				Description
/// originName			Path and name of origin object
/// origin				Transform of the object the sensor is attached to
/// radius				Maximum visibility distance of the sensor
/// r2					Radius squared
/// losCheck			Check LOS after initial tests are complete
/// showSensorRegion	Displays the sensor visibility region when true
/// sensorRegion		Current sensor visibility region
/// 
/// Author:		Brian Brookwell (BRB)
/// </summary>

[System.Serializable]
public abstract class SNSSensor: SNSBase {
	protected const float EPSILON = 1e-4f;
	protected const int NAME_WIDTH = 100;

	public float radius;
	public bool losCheck = false;

	[System.NonSerialized]protected float r2;

	/// <summary>
	/// Sets up the parent game object for the sensor by initializing non-serialized values
	/// based on saved public values
	/// </summary>
	/// 
	/// Date		Author	Description
	/// 2017-10-28	BRB		Changes the gameObject associated with this Sensor

	override protected void Init () {
		base.Init();

		r2 = radius * radius + radius * EPSILON;

		HandleSensorRegion();
	}

	/// <summary>
	/// Handles all changes to the Sensor Region of a Sensor
	/// </summary>
	/// 
	/// Date		Author	Description
	/// 2017-10-12	BRB		Initial Testing

	protected void HandleSensorRegion () {
		transform.localScale = Scale();		// Apply radius as scaling
	}

	/// <summary>
	/// Changes the 3D scale of the sensor's visibility object.  The default is uniform scaling
	/// by a fixed amount in X, Y and Z
	/// </summary>
	/// 
	/// Date		Author	Description
	/// 2017-10-12	BRB		Initial Testing

	virtual protected Vector3 Scale() {
		return new Vector3 (radius, radius, radius);
	}

	/// <summary>
	/// Gets the asset path for a Transform (used as origin)
	/// </summary>
	/// <returns>The transform path as a string</returns>
	/// <param name="transform">Transform for which path is being generated</param>
	/// 
	/// Date		Author	Description
	/// 2017-10-12	BRB		Work around for Assets not being able to save asset objects

	private static string GetTransformPath(Transform transform) {
		string path = transform.name;

		while (transform.parent != null) {
			transform = transform.parent;
			path = transform.name + "/" + path;
		}

		return path;
	}

	/// <summary>Change sthe visibility radius and computes the radius squared</summary>
	/// <param name="r">Visibility radius of the sensor</param>
	/// Date		Author	Description
	/// 
	/// 2017-10-08	BRB		Initial Testing

	public void SetRadius (float r) { 
		radius = r; 
		r2 = r * r + r * EPSILON;

		HandleSensorRegion();
	}

	/// <summary>Uses a raycast to check whether the target is in LOS</summary>
	/// <returns><c>true</c> if target was in LOS, <c>false</c> otherwise.</returns>
	/// <param name="target">Target being checked for LOS</param>
	/// <param name="heading">Heading of ray to use to check LOS</param>
	/// <param name="source">Source position of the ray</param>
	/// 
	/// Date		Author	Description
	/// 2017-10-08	BRB		Initial Testing

	protected bool RayCheck (Transform target, Vector3 heading, Vector3 source) {
		RaycastHit hit = new RaycastHit();
		if (Physics.Raycast (source, heading.normalized, out hit, radius)) {
			return hit.collider.transform == target;
		}
		return false;
	}
}
