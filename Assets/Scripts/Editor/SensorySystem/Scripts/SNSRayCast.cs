using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Determines whether the target can be seen by firing a ray towards it.  The maximum distance is
/// set by the radius of the sensor which is set using SetUp (in Sensor)
/// 
/// Author:	Brian Brookwell (BRB)
/// </summary>

public class SNSRayCast : SNSSphere {

	/// <summary>Determines whether this sensor can see the indicated target </summary>
	/// <param name="target">Transform of the target being tested</param>
	/// <returns>True if the target can be seen from the origin, false otherwise</returns>
	/// 
	/// Date		Author	Description
	/// 2017-10-06	BRB		Initial Testing
	/// 2017-10-08	BRB		Converted to use common RayCheck from Sensor

	override public bool CanSee (Transform target) {
		Vector3 heading = target.position - gameObject.transform.position;

		if (heading.sqrMagnitude <= r2)
			return RayCheck (target, heading, gameObject.transform.position);
		return false;
	}

	/// <summary>Determines whether this sensor can see the indicated target </summary>
	/// <param name="target">GameObject of the target being tested</param>
	/// <returns>True if the target can be seen from the origin, false otherwise</returns>
	/// 
	/// Date		Author	Description
	/// 2017-10-06	BRB		Initial Testing
	/// 2017-10-08	BRB		Converted to use common RayCheck from Sensor

	override public bool CanSee (GameObject target) {
		Vector3 heading = target.transform.position - gameObject.transform.position;

		if (heading.sqrMagnitude <= r2)
			return RayCheck (target.transform, heading, gameObject.transform.position);
		return false;
	}

	/// <summary>Debug output of Ray Cast Sensor</summary>
	/// <returns>Debug string giving Ray Cast Sensor settings</returns>
	/// 
	/// Date		Author	Description
	/// 2017-10-07	BRB		Initial Testing

	override public string ToString() {
		return "Ray Cast " + name;
	}
		
	/// <summary>
	/// Creates the sphereical region that the ray cast can appear in
	/// </summary>
	/// <returns>The visibility region for the Ray Cast</returns>
	/// 
	/// Date		Author	Description
	/// 2017-10-17	BRB		Initial Testing

	override public Mesh CreateMesh () {
		return base.CreateMesh();
	}
}
