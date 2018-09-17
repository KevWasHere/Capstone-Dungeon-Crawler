using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Creates a hemispheric sensor
///
/// Author:	Brian Brookwell (BRB)
/// </summary>

public class SNSHemisphere : SNSSensor {
	private static Mesh prefab = null;

	/// <summary>Determines whether this sensor can see the indicated target </summary>
	/// <param name="target">Transform of the target being tested</param>
	/// <returns>True if the target can be seen from the origin, false otherwise</returns>
	/// 
	/// Date		Author	Description
	/// 2017-10-06	BRB		Initial Testing
	/// 2017-10-08	BRB		Converted to add position offset and LOS Check
	/// 2017-10-21	BRB		Converted to use GameObject as base instead of Asset

	override public bool CanSee (Transform target) {
		Vector3 delta = target.position - gameObject.transform.position;
		if (delta.sqrMagnitude <= r2 && Vector3.Dot (delta.normalized, gameObject.transform.forward) >= -EPSILON)
			return !losCheck || RayCheck (target, delta, gameObject.transform.position);
		return false;
	}

	/// <summary>Determines whether this sensor can see the indicated target </summary>
	/// <param name="target">GameObject of the target being tested</param>
	/// <returns>True if the target can be seen from the origin, false otherwise</returns>
	/// 
	/// Date		Author	Description
	/// 2017-10-06	BRB		Initial Testing
	/// 2017-10-08	BRB		Converted to add position offset and LOS Check

	override public bool CanSee (GameObject target) {
		return CanSee (target.transform);
	}

	/// <summary>Debug output of Hemisphere Sensor</summary>
	/// <returns>Debug string giving Hemisphere Sensor settings</returns>
	/// 
	/// Date		Author	Description
	/// 
	/// 2017-10-07	BRB		Initial Testing

	override public string ToString() {
		return "Hemisphere " + name;
	}

	/// <summary>
	/// Returns the mesh associated with the hemisphere sensor
	/// </summary>
	/// <returns>The mesh for a hemisphere.</returns>
	/// 
	/// 2017-10-07	BRB		Initial Testing

	override public Mesh CreateMesh () {
		if (prefab == null) {
			GameObject hemisphere = Resources.Load ("SensorySystem/Meshes/Hemisphere") as GameObject;
			prefab = hemisphere.GetComponent<MeshFilter>().sharedMesh;
		}
		return prefab;
	}
}
